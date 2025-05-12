var selectedPoint = null;

var stage = new Konva.Stage({
    container: 'container',
    width: window.innerWidth - 300,
    height: window.innerHeight
});

var layer = new Konva.Layer();
stage.add(layer);

var entities = {};
var commentGroups = {};

function loadPoints() {
    axios.get('/api/v1/points').then(function (resp) {
        layer.destroyChildren();
        entities = {};
        commentGroups = {};
        resp.data.forEach(function (p) {

            var group = new Konva.Group({
                x: p.x,
                y: p.y,
                draggable: true
            });


            entities[p.id] = { point: p, shape: group };
            layer.add(group);

            group.on('click', function () { selectPoint(p.id) });
            group.on('dragmove', function () { p.x = group.x(); p.y = group.y(); layer.batchDraw(); });
            group.on('dragend', function () { selectPoint(p.id); axios.put('/api/v1/points/' + p.id, p); });

            drawPoint(p.id);
            drawComments(p.id);
        });
        layer.draw();
    });
}

function drawPoint(pointId) {
    var entity = entities[pointId];
    var point = entity.point;
    var shape = entity.shape;

    var circle = new Konva.Circle(
    {
        radius: point.radius,
        fill: point.colorHEX,
        stroke: '#000',
        strokeWidth: 1,
    });

    circle.on('dblclick', function () { axios.delete('/api/v1/points/' + pointId).then(loadPoints); });
    shape.add(circle);
}

function drawComments(pointId) {
    var entity = entities[pointId];
    if (!entity)
        return;

    var point = entity.point;
    var shape = entity.shape;

    shape.getChildren(c => c.getClassName() != 'Circle').forEach(function (c) {
        c.destroy();
    });

    var spacing = 6;
    var commentWidth = point.radius * 6;
    var baseY = point.radius + 20;

    point.comments.forEach(function (c, i) {
        var yPos = baseY + i * (28 + spacing);

        var text = new Konva.Text({
            text: c.text,
            fontSize: 18,
            fontFamily: 'Calibri',
            fill: '#000',
            padding: 5,
            align: 'center'
        });

        var deleteX = new Konva.Text({
            text: '✖',
            fontSize: 18,
            x: text.width() + 3,
            fill: '#000',
            align: 'center',
            padding: 5
        });

        var commentGroup = new Konva.Group({
            x: text.width() / 2 * (-1),
            y: yPos,
            width: text.width(),
            height: 28
        });

        var background = new Konva.Rect({
            width: text.width(),
            height: 28,
            fill: c.colorHEX,
            stroke: '#000',
            strokeWidth: 1,
        });

        deleteX.on('click', function () {
            point.comments = point.comments.filter(function (comment) {
                return comment.id !== c.id;
            });
            updateComments(point.id, point.comments);
        });

        commentGroup.add(background);
        commentGroup.add(text);
        commentGroup.add(deleteX);
        shape.add(commentGroup);
    });
}

function selectPoint(pointId) {
    selectedPoint = entities[pointId].point;
    document.getElementById("point-color").value = selectedPoint.colorHEX;
    document.getElementById("point-radius").value = selectedPoint.radius;
}

function updateComments(pointId, comments) {
    axios.patch('/api/v1/points/' + pointId + '/comments', comments).then(loadPoints);
}

document.getElementById('point-radius').addEventListener('input', function () {
    if (!selectedPoint)
        return;

    selectedPoint.radius = parseInt(document.getElementById('point-radius').value);
    axios.put('/api/v1/points/' + selectedPoint.id, selectedPoint).then(loadPoints);
});


document.getElementById('point-color').addEventListener('input', function () {
    if (!selectedPoint)
        return;

    selectedPoint.colorHEX = document.getElementById('point-color').value;
    var shape = entities[selectedPoint.id].shape;
    shape.getChildren()[0].setAttr('fill', selectedPoint.colorHEX);
});

document.getElementById('point-color').addEventListener('change', function () {
    if (!selectedPoint)
        return;

    selectedPoint.colorHEX = document.getElementById('point-color').value;
    axios.put('/api/v1/points/' + selectedPoint.id, selectedPoint).then(loadPoints);
});

document.getElementById('add-comment').addEventListener('click', function () {
    if (!selectedPoint)
        return;

    var commentText = document.getElementById('point-comment').value;
    var commentHex = document.getElementById('comment-color').value;

    var comment = { text: commentText, colorHEX: commentHex };
    selectedPoint.comments.push(comment);

    updateComments(selectedPoint.id, selectedPoint.comments);
});

document.getElementById('create-point').addEventListener('click', function () {
    var x = + document.getElementById('new-x').value;
    var y = + document.getElementById('new-y').value;
    var r = + document.getElementById('new-radius').value;
    var col = document.getElementById('new-color').value;

    var point = { x: x, y: y, radius: r, colorHEX: col, comments: [] };
    axios.post('/api/v1/points', point).then(loadPoints);
});

loadPoints();