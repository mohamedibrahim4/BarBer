function updatemark(arg) {
    var abc = ""
    //Iterating the collection of checkboxes which checked marked
    $('input[type=checkbox]').each(function () {
        var someObj = {};
        someObj.fruitsGranted = [];
        someObj.fruitsDenied = [];

        if (this.checked) {
            abc = abc + $(this).attr('id') + ",";
            $('#SkillID').val(abc);
        }
    });
    console.log(abc);
}
//function sum() {
//    var TotalAmount = document.getElementById('TotalAmount').value;
//    var PaidAmount = document.getElementById('PaidAmount').value;
//    var discount = document.getElementById('discount').value;
//    var Vat = document.getElementById('Vat').value;

//    if (TotalAmount == "")
//        TotalAmount = 0;
//    if (PaidAmount == "")
//        PaidAmount = 0;
//    if (discount == "")
//        discount = 0;
//    if (Vat == "")
//        Vat = 0;


//    var RemainingAmount = parseFloat(TotalAmount) - parseFloat(PaidAmount) - parseFloat(discount) - parseFloat(Vat);
//    if (!isNaN(RemainingAmount)) {
//        document.getElementById('RemainingAmount').value = RemainingAmount;
//    }
//var abc = 0
function sum() {
    var abc = 0
   

    if (Amount == "")
        Amount = 0;

    if (AmountWithVat == "")
        AmountWithVat = 0;
    $('#Amount').val(0);
    $('#Vat').val(0);
    $('#AmountWithVat').val(0);

    $('input[type=checkbox]').each(function () {
     
        if (this.checked) {
            debugger;
            var Cutter = ":";
            var price = $("#" + parseInt($(this).attr('id'))).next().text();
            let result = price.substring(parseInt(price.indexOf(Cutter))+1);
            abc = parseFloat(abc) + parseFloat(result) ;
            $('#Amount').val(abc);
            let vat = .05
            var vatAmount = parseFloat(abc) * vat;
            $('#Vat').val(vatAmount.toFixed(2));

            var AmountWithVat = parseFloat(abc) + parseFloat(vatAmount);
            $('#AmountWithVat').val(AmountWithVat);


        }
    });



}