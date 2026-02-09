const appHelper = function () {
    const onCurrencyInput = function () {
        // Remove non-numeric characters except decimal point
        let sanitizedValue = $(this).val().replace(/[^0-9.]/g, '');

        // If there are multiple decimal points, retain the first one only
        let decimalCheck = sanitizedValue.split('.');
        if (decimalCheck.length > 2) {
            sanitizedValue = decimalCheck[0] + '.' + decimalCheck.slice(1).join('');
        }

        // If the value starts with a decimal point, prepend a '0'
        if (sanitizedValue.charAt(0) === '.') {
            sanitizedValue = '0' + sanitizedValue;
        }

        // Limit to two decimal places
        let decimalIndex = sanitizedValue.indexOf('.');
        if (decimalIndex !== -1) {
            let integerPart = sanitizedValue.substring(0, decimalIndex);
            let decimalPart = sanitizedValue.substring(decimalIndex + 1, decimalIndex + 3); // Limit to two decimal places
            sanitizedValue = integerPart + '.' + decimalPart;
        }

        // Update the input field value
        $(this).val(sanitizedValue);
    }

    const convertToDate = function (text) {
        if (text == '' || text == null) {
            return null
        }
        const value = Date.parse(text)
        return new Date(value)
    }

    const convertToApiDate = function (text) {

        if (text == null || text == '') {
            return null
        }

        const [day, month, year] = text.split('-').map(Number);
        return `${year}-${month}-${day}`;
    }

    const convertDateToDMY = function (date) {

        if (date == null || date == '') {
            return '';
        }

        const day = date.getDate() < 10 ? `0${date.getDate()}` : `${date.getDate()}`
        const month = date.getMonth() + 1 < 10 ? `0${date.getMonth() + 1}` : `${date.getMonth() + 1}`
        return `${day}-${month }-${date.getFullYear()}`;
    }

    const formatPrice = function (price) {
        const formattedPrice = new Intl.NumberFormat('en-US', {
            style: 'decimal',
            minimumFractionDigits: 2,
            maximumFractionDigits: 2,
        }).format(price);

        return formattedPrice
    }

    function getShortMonthName(monthIndex) {
        const shortMonthNames = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
        return shortMonthNames[monthIndex];
    }

    function dateToFormat(date, format = "DD-MM-YYYY HH:mm") {
        return moment(date).format(format)
    }


    return {
        onCurrencyInput,
        convertToDate,
        convertToApiDate,
        formatPrice,
        convertDateToDMY,
        dateToFormat
    }
}()

