$.validator.addMethod("notZero", function (value, element) {
    if ($(element).data('select2') !== undefined && $(element).hasOwnProperty('multiple')) {
        return value.length > 0;
    }
    return this.optional(element) || value !== "0";
}, "Please select a valid option.");