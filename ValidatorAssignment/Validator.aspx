<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Validator.aspx.cs" Inherits="ValidatorAssignment.Validator" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <script type="text/javascript">
        function Checks() {
            var name = document.getElementById("txtName").value;

            var familyName = document.getElementById("txtFamilyName").value;

            var address = document.getElementById("txtAddress").value;

            var city = document.getElementById("txtCity").value;

            var zipCode = document.getElementById("txtZipCode").value;

            var phone = document.getElementById("txtPhone").value;

            var email = document.getElementById("txtEmail").value;

            var errors = [];

            // Client-side validation

            if (name.trim() === '') {
                errors.push("Name.");
                document.getElementById("rfvName").style.display = "inline";
            } else {
                document.getElementById("rfvName").style.display = "none";
            }

            if (familyName.trim() === '') {
                errors.push("Family.");
                document.getElementById("rfvFname").style.display = "inline";
            } else {
                document.getElementById("rfvFname").style.display = "none";
            }

            if (familyName.trim() === name.trim()) {
                errors.push("Family name should differ from Name.");
            }

            if (address.trim().length < 2) {
                errors.push("Address.");
                document.getElementById("rfvAddress").style.display = "inline";
            } else {
                document.getElementById("rfvAddress").style.display = "none";
            }

            if (city.trim().length < 2) {
                errors.push("City.");
                document.getElementById("rfvCity").style.display = "inline";
            } else {
                document.getElementById("rfvCity").style.display = "none";
            }

            if (!/^\d{5}$/.test(zipCode)) {
                errors.push("Zip Code.");
                document.getElementById("rfvZip").style.display = "inline";
            } else {
                document.getElementById("rfvZip").style.display = "none";
            }

            if (!/^\d{2,3}-\d{7}$/.test(phone)) {
                errors.push("Phone No.");
                document.getElementById("rfvPhone").style.display = "inline";
            } else {
                document.getElementById("rfvPhone").style.display = "none";
            }

            if (!/\S+@\S+\.\S+/.test(email)) {
                errors.push("Email.");
                document.getElementById("rfvEmail").style.display = "inline";
            } else {
                document.getElementById("rfvEmail").style.display = "none";
            }

            var errorList = document.getElementById("errorList");

            errorList.innerHTML = "";

            if (errors.length > 0) {
                errors.forEach(function (error) {
                    var li = document.createElement("li");
                    li.textContent = error;
                    errorList.appendChild(li);
                });
                alert("Validation Errors:\n" + errors.join("\n"));
                return false;
            }
            return true;
        }
      </script>
</head>
<body>
    <form id="form1" runat="server">
             <h2>Insert Your Details</h2>
        Name:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        &nbsp;<asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" ErrorMessage="Name" ForeColor="Red" Display="Dynamic" ValidationGroup="validerrorgrp">*</asp:RequiredFieldValidator>
        <br />
        <br />
        Family Name:&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtFamilyName" runat="server"></asp:TextBox>
        &nbsp;<asp:RequiredFieldValidator ID="rfvFname" runat="server" ControlToValidate="txtFamilyName" ForeColor="Red" ErrorMessage="Family" Display="Dynamic" ValidationGroup="validerrorgrp">*</asp:RequiredFieldValidator>
        &nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtFamilyName" ControlToCompare="txtName" Operator="NotEqual" ErrorMessage="Differ from Name."></asp:CompareValidator>
        <br /><br />
        Address: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtAddress" runat="server"></asp:TextBox>
        &nbsp;<asp:RequiredFieldValidator ID="rfvAddress" runat="server" ControlToValidate="txtAddress" ErrorMessage="Address" ForeColor="Red" Display="Dynamic" ValidationGroup="validerrorgrp">*</asp:RequiredFieldValidator>
        &nbsp;<asp:RegularExpressionValidator ID="revAddress" runat="server" ControlToValidate="txtAddress" ErrorMessage="Address must be at least 2 letters." ValidationExpression=".{2,}$" Display="Dynamic"/><br />
        <br />
        City:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtCity" runat="server"></asp:TextBox>
        &nbsp;
        <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtCity" ErrorMessage="City" ForeColor="Red" Display="Dynamic" ValidationGroup="validerrorgrp">*</asp:RequiredFieldValidator>
        &nbsp;<asp:RegularExpressionValidator ID="revCity" runat="server" ControlToValidate="txtCity" ErrorMessage="City must be at least 2 letters." ValidationExpression=".{2,}$" Display="Dynamic" /><br />
        <br />
        Zip Code:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtZipCode" runat="server"></asp:TextBox>
        &nbsp;<asp:RequiredFieldValidator ID="rfvZip" runat="server" ControlToValidate="txtZipCode" ErrorMessage="Zip" ForeColor="Red" Display="Dynamic" ValidationGroup="validerrorgrp">*</asp:RequiredFieldValidator>
        &nbsp;<asp:RegularExpressionValidator ID="revZipCode" runat="server" ControlToValidate="txtZipCode" ErrorMessage="Zip Code must be 5 digits." ValidationExpression="\d{5}$" Display="Dynamic" /><br />
        <br />
        Phone: &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtPhone" runat="server"></asp:TextBox>
        &nbsp;
        <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtPhone" ErrorMessage="Phone " ForeColor="Red" Display="Dynamic" ValidationGroup="validerrorgrp">*</asp:RequiredFieldValidator>
        &nbsp;<asp:RegularExpressionValidator ID="revPhone" runat="server" ControlToValidate="txtPhone" ErrorMessage="Phone no is must be correct form " ValidationExpression="^\d{2,3}-\d{7}$" Display="Dynamic"/><br />
        <br />
        E-mail:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
        &nbsp;
        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email" ForeColor="Red" ValidationGroup="validerrorgrp" Display="Dynamic">*</asp:RequiredFieldValidator>
        &nbsp;<asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ErrorMessage="Email is not valid." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" Display="Dynamic" /><br />
        <br />
        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ValidationGroup="validerrorgrp" ForeColor="Red"  />
        <asp:Label ID="lblmsg" runat="server" ForeColor="Green" Visible="false" Text=" "></asp:Label>
        <asp:Button ID="btnCheck" runat="server" Text="Check" Style="float: right;" OnClick="btnCheck_Click"  ValidationGroup="validerrorgrp" OnClientClick="return Checks();" />
        <ul id="errorList" style="color: red;"></ul>
    </form>
</body>
</html>
