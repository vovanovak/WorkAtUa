function CheckMyText(sender, args) {
    if (args.Value != "") {
        args.IsValid = false;
        return;
    }

    var re = new RegExp("^[a-zA-Z]+$");

    if (!re.test(args.Value)) {
        args.IsValid = false;
        return;
    }

    args.IsValid = true;
    return;

}
function CheckMyPhone(sender, args) {
    args.IsValid = args.Value.match("^[0-9]+$");
    return;
}
function CheckMyEmail(sender, args) {
    args.IsValid = !args.Value.match("^[\w-]+@([\w-]+\.)+[\w-]+$");
    return;
}
function CheckMyPass(sender, args) {
    args.IsValid = args.Value.match("^[a-zA-Z0-9]+$");
    return;
}