$.validator.addMethod("lessthan", function (value, element, params) {
    const form = $(element).parents('form');
    const name = $(element).attr('name');
    const displayName = $(form).find('label').filter((i, x) => $(x).attr('for') === name).text();

    params[0] = displayName;

    const otherElement = $(form).find('input').filter((i, x) => $(x).attr('name') === params[2]); 
    if (otherElement.length == 0) return true;

    if (otherElement.attr("type") !== $(element).attr("type")) return true;

    const otherVal = otherElement.val();
    if (typeof (otherVal) === "undefined" || otherVal.trim() === "") return true;

    switch ($(element).attr("type")) {
        case "range":
        case "number":
            const number = Number.parseInt(value);
            const otherNumber = Number.parseInt(otherVal);;
            return number < otherNumber;
        case "date":
        case "datetime":
        case "time":
        case "datetime-local":
            const date = Date.parse(value);
            const otherDate = Date.parse(otherVal);
            return date - otherDate >= 0;
        default:
            return true;
    }
});

$.validator.unobtrusive.adapters.add("lessthan", ["other"], function (options) {
    const otherDisplayName = $(options.form).find('label').filter((i, x) => $(x).attr('for') === options.params.other)
        .text();

    if (otherDisplayName === null || typeof (otherDisplayName) == "undefined") {
        options.rules["lessthan"] = [options.params.other, options.params.other];
    } else {
        options.rules["lessthan"] = [options.params.other, otherDisplayName, options.params.other];
    }

    options.messages["lessthan"] = options.message;
});