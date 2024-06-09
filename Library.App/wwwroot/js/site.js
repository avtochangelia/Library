function showDetailsBookModal(id, title, description, image, rating, status, authors) {
    $('#bookId').text(id);
    $('#bookTitle').text(title);
    $('#bookDescription').text(description);
    $('#bookImage').attr('src', image);
    $('#bookRating').text(rating);
    $('#bookRating').text(rating);
    $('#bookStatus').text(status);
    $('#bookAuthors').text(authors);
    $('#detailsBookModal').modal('show');
}

function showCreateBookModal() {
    $('#createBookModal').modal('show');
}

function showUpdateBookModal(id, title, description, image, rating, authorIds) {
    $('#updateBookId').val(id);
    $('#updateTitle').val(title);
    $('#updateDescription').val(description);
    $('#updateImage').val(image);
    $('#updateRating').val(rating);
    $('#updateAuthorIds').val(authorIds);
    $('#updateBookModal').modal('show');
}

function showDeleteBookModal(id) {
    $('#deleteBookId').val(id);
    $('#deleteBookModal').modal('show');
}

function showTakeOutBookModal(id) {
    $('#takeOutBookId').val(id);
    $('#takeOutBookModal').modal('show');
}

function showBringInBookModal(id) {
    $('#bringInBookId').val(id);
    $('#bringInBookModal').modal('show');
}

$('#cancelDetailsBook').click(function () {
    $('#detailsBookModal').modal('hide');
});

$('#cancelCreateBook').click(function () {
    $('#createBookModal').modal('hide');
});

$('#cancelUpdateBook').click(function () {
    $('#updateBookModal').modal('hide');
});

$('#cancelDeleteBook').click(function () {
    $('#deleteBookModal').modal('hide');
});

$('#cancelTakeOutBook').click(function () {
    $('#takeOutBookModal').modal('hide');
});

$('#cancelBringInBook').click(function () {
    $('#bringInBookModal').modal('hide');
});

function showCreateAuthorModal() {
    $('#createAuthorModal').modal('show');
}

function showUpdateAuthorModal(id, firstName, lastName, dateOfBirth) {
    $('#updateAuthorId').val(id);
    $('#updateFirstName').val(firstName);
    $('#updateLastName').val(lastName);
    $('#updateDateOfBirth').val(new Date(dateOfBirth).toISOString().split('T')[0]);
    $('#updateAuthorModal').modal('show');
}

function showDeleteAuthorModal(id) {
    $('#deleteAuthorId').val(id);
    $('#deleteAuthorModal').modal('show');
}

$('#cancelCreateAuthor').click(function () {
    $('#createAuthorModal').modal('hide');
});

$('#cancelUpdateAuthor').click(function () {
    $('#updateAuthorModal').modal('hide');
});

$('#cancelDeleteAuthor').click(function () {
    $('#deleteAuthorModal').modal('hide');
});