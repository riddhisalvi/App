

        var tmr, tmr1, tmr2, tmr3;
        
      

        function StopTimer1(btnhdnEnableFields1) {
            clearTimeout(tmr);
            clearTimeout(tmr1);
            document.getElementById(btnhdnEnableFields1).click();
          }

        function StopTimer(lblid, btnDeal) {
            clearTimeout(tmr);
            clearTimeout(tmr1);
           
            // document.getElementById(lblid).innerHTML = "";
            //document.getElementById(btnDeal).disabled = true;

        }


        function InitializeTimer(lblid, ValidityTime, btnDeal, btnhdnEnableFields) {

            if (ValidityTime == "") ValidityTime = 60;
            document.getElementById(lblid).innerHTML = Pad(ValidityTime);

            if (ValidityTime < 20) { document.getElementById(lblid).style.color = "red"; }
            ValidityTime = ValidityTime - 1;
            //   document.getElementById(lblid).style.fontSize =50; 
            if (ValidityTime <= 0) {
                clearTimeout(tmr);
                document.getElementById(lblid).innerHTML = "";
                //document.getElementById(lblPrice1).innerHTML = "";
                document.getElementById(btnDeal).disabled = true;
                document.getElementById(btnhdnEnableFields).click();
            }
            else {
                tmr = self.setTimeout("InitializeTimer('" + lblid + "','" + ValidityTime + "','" + btnDeal + "','" + btnhdnEnableFields + "');", 1000);
            }


        }

        function InitializeTimer1(lblid1, ValidityTime1, btnhdnEnableFields) {
            if (ValidityTime1 == "") ValidityTime1 = 60;
            document.getElementById(lblid1).innerHTML = Pad(ValidityTime1);
            if (ValidityTime1 < 20) { document.getElementById(lblid1).style.color = "red"; }
            ValidityTime1 = ValidityTime1 - 1;
            if (ValidityTime1 <= 0) {
                clearTimeout(tmr1);
                document.getElementById(lblid1).innerHTML = "";
                document.getElementById(btnhdnEnableFields).click();
            }
            else {
                tmr1 = self.setTimeout("InitializeTimer1('" + lblid1 + "','" + ValidityTime1 + "','" + btnhdnEnableFields + "');", 1000);
            }

        }
        function Pad(number) {
            if (number < 10) number = 0 + "" + number; return number;
        }

        function SpecialCharacterNotAllowed(tbControl) {
            var ControlText = document.getElementById(tbControl).value
            var C, d;
            CheckStartSpace(tbControl)
            if (ControlText.length > 0) {
                for (i = 0; i < ControlText.length; i++) {
                    d = ControlText.charAt(i)

                    if (((d >= 'a') && (d <= 'z')) || ((d >= 'A') && (d <= 'Z')) || ((d <= '') && (d >= ' ')) || ((d >= '0') && (d <= '9')) || (d == '-') || (d == ' ')) {
                    }
                    else {
                        C = ControlText.substring(0, i)
                        document.getElementById(tbControl).value = C
                        document.getElementById(tbControl).focus();

                        return false
                    }
                }
            }

        }
        function CheckStartSpace(tbControl) {
            var ControlText = document.getElementById(tbControl).value;
            var C = ControlText.substring(0, ControlText.length);

            if (ControlText.charAt(0) == ' ') {
                var pos = 0;
                for (var i = 0; i < ControlText.length; i++) {
                    if (ControlText.charAt(i) == ' ')
                        pos = pos + 1;
                    if (ControlText.charAt(i) != '')
                        break;
                }
                document.getElementById(tbControl).value = ControlText.substring(pos, ControlText.length);
            }

        }
    