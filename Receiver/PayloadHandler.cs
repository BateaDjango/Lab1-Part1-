using MyLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Receiver
{
    public class PayloadHandler
    {
        public static void Handle(byte[] payloadBytes)
        {
            var payloadString = Encoding.UTF8.GetString(payloadBytes);
            var payload = JsonConvert.DeserializeObject<Payload>(payloadString);

            Form1 mainForm = (Form1)Application.OpenForms[0];
            //!!!!!!Aici se afiseaza mesajul !

            mainForm.Invoke(new MethodInvoker(() =>
            {
                mainForm.Write_in_textBox(payload.Message);
            }));
            //mainForm.Write_in_textBox(payload.Message);

        } 

    }
}
