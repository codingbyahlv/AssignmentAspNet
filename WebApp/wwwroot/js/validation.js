const formErrorHandler = (e, validationResult) => {
    console.log("Form Error Handler körs!")
    let spanElement = document.querySelector(`[data-valmsg-for="${e.target.name}"]`)
    if (validationResult) {
        e.target.classList.remove("input-validation-error")
        spanElement.classList.remove("field-validation-error")
        spanElement.classList.add("field-validation-valid")
        spanElement.innerHTML = "";
    } else {
        e.target.classList.add("input-validation-error")
        spanElement.classList.add("field-validation-error")
        spanElement.classList.remove("field-validation-valid")
        spanElement.innerHTML = e.target.dataset.valRequired;
    }
}


//value, min längd(sätts till 2 som standard om inget annat anges)
const lengthValidatior = (value, minLength = 2) => {
    if (value.length >= minLength) {
        return true
    }

    return false
}


const compareValidator = (value, compareValue) => {
    if (value === compareValue) {
        return true
    }
    return false
}


const checkedValidator = (element) => {
    if (element.checked) {
        return true
    }
    return false
}


const textValidator = (e) => {
    formErrorHandler(e, lengthValidatior(e.target.value))
}



const emailValidator = (e) => {
    //två tecken innan @ två tecken innan . och två tecken efter .
    const regEx = /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/
    formErrorHandler(e, regEx.test(e.target.value))
}


const passwordValidator = (e) => {
    if (e.target.dataset.valEqualtoOther !== undefined) {
        formErrorHandler(e, compareValidator(e.target.value, document.getElementsByName(e.target.dataset.valEqualtoOther.replace('*', 'Form'))[0].value))

    } else {
        //min 8 tecken, min 1 liten bokstav, min 1 siffra, min 1 specialtecken
        const regEx = /^(?=.*[a-z])(?=.*\d)(?=.*[!@#$%^&*()_+])[A-Za-z\d!@#$%^&*()_+]{8,}$/;
        formErrorHandler(e, regEx.test(e.target.value))
    }
}


const checkboxValidator = (e) => {
    formErrorHandler(e, checkedValidator(e.target))
}


let forms = document.querySelectorAll("form")
let inputs = forms[0].querySelectorAll("input")

inputs.forEach(input => {
    if (input.dataset.val === "true") {
        if (input.type === "checkbox") {
            input.addEventListener("change", (e) => {
                checkboxValidator(e)
            })
        } else {
            input.addEventListener("keyup", (e) => {
                switch (e.target.type) {
                    case "text":
                        textValidator(e)
                        break;
                    case "email":
                        emailValidator(e)
                        break;
                    case "password":
                        passwordValidator(e)
                }
            })
        }
    }
})