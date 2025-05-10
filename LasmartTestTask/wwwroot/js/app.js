(function () {
    var stage = new Konva.Stage({
        container: 'container',
        width: window.innerWidth - 300,
        height: window.innerHeight
    });
    var layer = new Konva.Layer();
    stage.add(layer);
    var points = {};
    var commentGroups = {};
    var selectedId = null;

    function drawComments(p) {
        // remove existing group
        if (commentGroups[p.id]) commentGroups[p.id].destroy();
        var group = new Konva.Group();
        p.comments = p.comments || [];
        p.comments.forEach(function (c, i) {
            var padding = 4;
            var text = new Konva.Text({
                text: c.text,
                fontSize: 14,
                x: p.x - p.radius,
                y: p.y + p.radius + i * 24 + 8,
                width: p.radius * 2,
                padding: padding,
                align: 'center'
            });
            var rect = new Konva.Rect({
                x: text.x(),
                y: text.y(),
                width: text.width(),
                height: text.height(),
                fill: c.colorHEX,
                cornerRadius: 4
            });
            // bring text above rect
            group.add(rect);
            group.add(text);

            // edit on click
            rect.on('click', function () {
                var newText = prompt('Edit comment:', c.text);
                if (newText !== null) {
                    c.text = newText;
                    updateComments(p.id, p.comments);
                }
            });
            // delete on dblclick
            rect.on('dblclick', function () {
                if (confirm('Delete this comment?')) {
                    p.comments = p.comments.filter(function (cc) { return cc !== c; });
                    updateComments(p.id, p.comments);
                }
            });
        });
        layer.add(group);
        commentGroups[p.id] = group;
    }

    function loadPoints() {
        axios.get('/api/v1/points').then(function (resp) {
            var data = resp.data;
            layer.destroyChildren();
            points = {};
            commentGroups = {};
            data.forEach(function (p) {
                var circle = new Konva.Circle({
                    x: p.x,
                    y: p.y,
                    radius: p.radius,
                    fill: p.colorHEX,
                    stroke: '#000',
                    strokeWidth: 1,
                    draggable: true
                });
                circle.on('click', function () { selectPoint(p); });
                circle.on('dblclick', function () {
                    axios.delete('/api/v1/points/' + p.id).then(loadPoints);
                });
                circle.on('dragmove', function () {
                    p.x = circle.x();
                    p.y = circle.y();
                    drawComments(p);
                    layer.batchDraw();
                });
                circle.on('dragend', function () {
                    axios.put('/api/v1/points/' + p.id, p).then(loadPoints);
                });
                layer.add(circle);
                points[p.id] = { data: p, shape: circle };
                drawComments(p);
            });
            layer.draw();
        });
    }

    function selectPoint(p) {
        selectedId = p.id;
        document.getElementById('point-id').value = p.id;
        document.getElementById('point-x').value = p.x;
        document.getElementById('point-y').value = p.y;
        document.getElementById('point-radius').value = p.radius;
        document.getElementById('point-color').value = p.colorHEX;
    }

    function updateComments(id, comments) {
        axios.put('/api/v1/points/' + id, Object.assign({}, points[id].data, { comments: comments }))
            .then(loadPoints);
    }

    document.getElementById('create-point').addEventListener('click', function () {
        var x = Number(document.getElementById('new-x').value);
        var y = Number(document.getElementById('new-y').value);
        var radius = Number(document.getElementById('new-radius').value);
        var colorHEX = document.getElementById('new-color').value;
        axios.post('/api/v1/points', { x: x, y: y, radius: radius, colorHEX: colorHEX, comments: [] })
            .then(loadPoints);
    });

    document.getElementById('save-point').addEventListener('click', function () {
        if (!selectedId) return;
        var p = points[selectedId].data;
        p.x = Number(document.getElementById('point-x').value);
        p.y = Number(document.getElementById('point-y').value);
        p.radius = Number(document.getElementById('point-radius').value);
        p.colorHEX = document.getElementById('point-color').value;
        axios.put('/api/v1/points/' + selectedId, p).then(loadPoints);
    });

    // Click on stage background to add comment to selected point
    stage.on('contextmenu', function (e) {
        e.evt.preventDefault();
        if (!selectedId) return;
        var commentText = prompt('New comment text:');
        if (!commentText) return;
        var commentColor = prompt('Comment color (hex):', '#ffff00');
        if (!commentColor) return;
        var p = points[selectedId].data;
        p.comments = p.comments || [];
        p.comments.push({ id: 0, text: commentText, colorHEX: commentColor });
        updateComments(selectedId, p.comments);
    });

    loadPoints();
})();