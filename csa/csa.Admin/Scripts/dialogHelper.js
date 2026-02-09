const dialogHelper = function () {
    const info = (message) => {
        Swal.fire({
            title: 'CSA',
            text: message,
            icon: 'info',
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'OK',
        })
    }

    const success = (message) => {
        Swal.fire({
            title: 'CSA',
            text: message,
            icon: 'success',
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'OK',
        })
    }

    const successAutoRedirect = (message,redirect) => {
        Swal.fire({
            title: 'CSA',
            text: message,
            icon: 'success',
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'OK',
            timer: 2000,
            timerProgressBar: true,
        }).then((result) => {
            window.location.href = redirect
        });
    }

    const confirmation = (message,callback) => {
        Swal.fire({
            title: 'CSA',
            text: message,
            icon: 'warning',
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Yes',
            cancelButtonText: 'No',
            showCancelButton: true,
        }).then((result) => {
            if (result.isConfirmed) {
                callback()
            }
        });
    }

    const confirmationHTML = (message, callback) => {
        Swal.fire({
            title: 'CSA',
            html: message,
            icon: 'warning',
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'Yes',
            cancelButtonText: 'No',
            showCancelButton: true,
        }).then((result) => {
            if (result.isConfirmed) {
                callback()
            }
        });
    }

    const error = (message) => {
        Swal.fire({
            title: 'CSA',
            text: message,
            icon: 'error',
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'OK',
        })
    }

    const errorHTML = (message) => {
        Swal.fire({
            title: 'CSA',
            html: message,
            icon: 'error',
            confirmButtonColor: '#3085d6',
            confirmButtonText: 'OK',
        })
    }

    return {
        info,
        error,
        success,
        successAutoRedirect,
        confirmation,
        confirmationHTML,
        errorHTML
    }
}()