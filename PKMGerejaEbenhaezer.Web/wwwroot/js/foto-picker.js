function fotoPicker(data) {
    const { inputContainer, modal, inputName, selectedClass, multi } = data;

    if (typeof (selectedClass) !== "string") throw new Error("selectedClass must be string");

    if (typeof (multi) !== "boolean") throw new Error("multi must be bool");

    const btnUploadFoto = $(modal).find('#uploadFoto');
    const btnPilih = $(modal).find('#buttonPilih');
    const fotoGrid = $(modal).find('#fotoGrid');

    let daftarFotoContainer = $(modal).find('.foto-container');
    let idTerpilih;
    let daftarIdTerpilih = [];

    const updateUI = () => {
        daftarFotoContainer.each(function (index, item) {
            const id = $(this).find('#fotoId').val();

            if (multi) {
                if (daftarIdTerpilih.indexOf(id) !== -1) {
                    $(this).addClass(selectedClass);
                } else {
                    $(this).removeClass(selectedClass);
                }
            } else {
                if (id === idTerpilih) {
                    $(this).addClass(selectedClass);
                } else {
                    $(this).removeClass(selectedClass);
                }
            }
        }); 
    }

    const onFotoClick = function () {
        const fotoId = $(this).find('#fotoId').val();

        if (multi) {
            const selected = $(this).hasClass(selectedClass);

            if (selected) {
                daftarIdTerpilih.splice(daftarIdTerpilih.indexOf(fotoId), 1);
            } else {
                daftarIdTerpilih.push(fotoId);
            }
        } else {
            if (idTerpilih !== fotoId) {
                idTerpilih = fotoId;
            }
        }

        updateUI();
    };

    const init = () => {
        daftarFotoContainer.each(function (index, item) {
            $(item).on('click', onFotoClick);
        });

        if (multi) {
            $(inputContainer).find('input').each((index, item) => {
                const id = $(item).val();

                daftarIdTerpilih.push(id);
            })
        } else {
            const input = $(inputContainer).find('input').first();

            if (input) {
                idTerpilih = input.val();
            }
        }

        updateUI();
    }

    btnUploadFoto.on('click', async function () {
        const formData = new FormData();
        const inputFile = $(modal).find('#formFile')[0].files[0];

        formData.append('formFile', inputFile);

        $('#formFileVal').html('');

        $(this).html('');
        $(this).append('Loading...');
        $(this).prop('disabled', true);

        const response = await fetch(
            '/Dashboard/Foto/Tambah?isJson=true',
            {
                credentials: 'same-origin',
                method: "POST",
                body: formData,
            }
        );

        if (response.ok) {
            const data = await response.json();

            const newFotoContainer = $(
                `<div class="col-3 p-1 foto-container">
                     <input type="hidden" id="fotoId" value="${data['id']}" />
                     <img class="img-thumbnail" style="width:100%; height:150px !important; object-fit:cover;" src="${makeGetUrl(data['id'])}" />
                 </div>`);

            fotoGrid.prepend(newFotoContainer);
            newFotoContainer.on('click', onFotoClick);
            daftarFotoContainer = $(modal).find('.foto-container');
        } else {
            const data = await response.json();

            $('#formFileVal').html('');
            $('#formFileVal').append(data['formFile']);
        }

        $(this).html('');
        $(this).append('Upload');
        $(this).prop('disabled', false);
    });

    btnPilih.on('click', function () {
        if (multi) {
            $(inputContainer).html('');

            for (let id of daftarIdTerpilih) {
                $(inputContainer).append(`<input name="${inputName}" id="${inputName}" value="${id}" hidden />`);
            }
        } else {
            $(inputContainer).html(`<input name="${inputName}" id="${inputName}" value="${idTerpilih}" hidden />`);
        }

        if ($('#dvPreview') !== null && $('#dvPreview') !== undefined) {
            $('#dvPreview').html('');
            if (multi) {
                for (let id of daftarIdTerpilih) {
                    $('#dvPreview').append(`<div class="col-3 p-2"><img class="img-thumbnail" src="${makeGetUrl(id)}"/></div>`);
                }
            } else {
                $('#dvPreview').append(`<img class="img-thumbnail" src="${makeGetUrl(idTerpilih)}"/>`);
            }
        }
    });

    init();
}

const makeGetUrl = (id) => '/Foto/Index/' + id;