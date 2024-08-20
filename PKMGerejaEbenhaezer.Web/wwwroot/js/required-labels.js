function requiredLabels() {
    const forms = $("form");
    const requiredLabel = "*";

    forms.each((index, form) => {
        const filter = (item) => $(item).attr("required") || $(item).attr("data-val-required");
        const requiredInputs = $(form).find("input").filter((index, item) => filter(item));
        const requiredSelects = $(form).find("select").filter((index, item) => filter(item));
        const requiredTextAreas = $(form).find("textarea").filter((index, item) => filter(item));

        const applyLabel = (item) => {
            const name = $(item).attr("id");
            const label = $(form).find(`label[for="${name}"]`);

            if (label && $(label).find("span.required").length === 0) {
                $(label).append(` <span class="required text-danger">${requiredLabel}</span>`);
            }
        };

        requiredInputs.each((index, input) => applyLabel(input));
        requiredSelects.each((index, select) => applyLabel(select));
        requiredTextAreas.each((index, textArea) => applyLabel(textArea));
    });
}