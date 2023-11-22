$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    datatable = $('#tblData').DataTable({
        "ajax": {url: '/admin/book/getall'},
        "columns": [
            { data: 'title', "width" : "25%" },
            { data: 'description', "width": "15%" },
            { data: 'price', "width": "5%" },
            { data: 'category.name', "width": "15%" },
            { data: 'author.fullName', "width": "15%" }
        ]
    });
}



