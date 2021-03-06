﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;





//there are actually two namespaces in this .cs (VATestPlugin, VATestWinampPlugin... see way down below) and each contain a class that would be considered a, 'plugin' (since each contain the necessary static functions to be a VoiceAttack plugin)

namespace VoiceAttackFO4Plugin
{

    public class VoiceAttackPlugin
    {

        
        //Version .21
        public static string VA_DisplayName()
        {
            return "Voice Attack FO4Plugin";  //this is what you will want displayed in dropdowns as well as in the log file to indicate the name of your plugin
        }

        public static string VA_DisplayInfo()
        {
            return "VoiceAttackPlugin\r\n\r\nTo be used with Fallout 4\r\n\r\n2014 VoiceAttack V.09";  //this is just extended info that you might want to give to the user.  note that you should format this up properly.
        } 

        public static Guid VA_Id()
        {
            
            return new Guid("{77679D45-2745-44B8-8777-D35E600E5B6E}");  //this id must be generated by YOU... it must be unique so VoiceAttack can identify and use the plugin
        }

        public static void VA_Init1(ref Dictionary<string, object> state, ref Dictionary<string, Int16?> conditions, ref Dictionary<string, string> textValues, ref Dictionary<string, object> extendedValues)
        {
            //this is where you can set up whatever session information that you want.  this will only be called once on voiceattack load, and it is called asynchronously.
            //the state parameter is a local copy of the state held on to by VoiceAttack.  In this case, the state will be a dictionary with zero items.  You can add as many items to hold on to as you want.
            //the conditions and textValues will also be empty.  You can add whatever you want to those lists and VoiceAttack will copy the values into its own lists



        }

        public static void VA_Exit1(ref Dictionary<string, object> state)
        {
            //this function gets called when VoiceAttack is closing (normally).  You would put your cleanup code in here, but be aware that your code must be robust enough to not absolutely depend on this function being called
            if (state.ContainsKey("myStateValue"))
            {
                //do some kind of file cleanup or whatever at this point
            }
        }

        public static void VA_Invoke1(String context, ref Dictionary<string, object> state, ref Dictionary<string, Int16?> conditions, ref Dictionary<string, string> textValues, ref Dictionary<string, object> extendedValues)
        {
            
            string strVariable = "";
            string strCommand = "";
            string strMsg = "";
            string strHOST = "";
            int strPORT = 8089;


            if (textValues.ContainsKey("Home_Location")) 
            {
                if (textValues["Home_Location"] == null) 
                {
                    strVariable = "Vault 111";
                }

                else
                {
                    strVariable = textValues["Home_Location"];
                   
                }

            }

            if (textValues.ContainsKey("FT_Location"))
            {
                if (textValues["FT_Location"] == null)
                {
                    strVariable = "Vault 111";
                }

                else
                {
                    strVariable = textValues["FT_Location"];

                }

            }

            if (textValues.ContainsKey("WeaponName"))
            {
                if (textValues["WeaponName"] == null)
                {
                    strVariable = "Test";
                }

                else
                {
                    strVariable = textValues["WeaponName"];

                }

            }
            


            if (textValues.ContainsKey("Direction_Location")) 
            {
                if (textValues["Direction_Location"] == null) 
                {
                    strVariable = "Vault 111";
                }

                else
                {
                    strVariable = textValues["Direction_Location"];

                }

            }

            if (textValues.ContainsKey("StationName"))
            {
                
                if (textValues["StationName"] == null)
                {
                    strVariable = "Diamond City Radio";
                }

                else
                {
                    strVariable = textValues["StationName"];

                }

            }

            if (textValues.ContainsKey("GrenadeName"))
            {

                if (textValues["GrenadeName"] == null)
                {
                    strVariable = "None";
                }

                else
                {
                    strVariable = textValues["GrenadeName"];

                }

            }



            if (textValues.ContainsKey("MonitorHP"))
            {
                if (textValues["MonitorHP"] == null) 
                {
                    strVariable = "MonitorHP";
                    strCommand = "MonitorHP";
                }

                else
                {
                    strVariable = textValues["MonitorHP"];
                   
                }

            }


            if (textValues.ContainsKey("HOST"))
            {
                if (textValues["HOST"] == null)
                {
                    strHOST = "127.0.0.1";
                }

                else
                {
                    strHOST = textValues["HOST"];

                }

            }



            if (textValues.ContainsKey("PORT"))
            {
                if (textValues["PORT"] == null)
                {
                    strPORT = Int32.Parse("8089");
                }

                else
                {
                    strPORT = Int32.Parse(textValues["PORT"]);
                }


            }

            if (textValues.ContainsKey("SentName"))
            {

                if (textValues["SentName"] == null)
                {
                    strVariable = "None";
                }

                else
                {
                    strVariable = textValues["SentName"];

                }

            }

            if (textValues.ContainsKey("SentCommand"))
            {
                if (textValues["SentCommand"] == null)
                {
                    textValues["ReturnMessage"] = "No Command Was given";
                    System.Windows.Forms.MessageBox.Show("No SentCommand value Was givens", "Error Voice Attack FO4Plugin",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error
                        );
                    return;
                }
                else
                {
                    strCommand = textValues["SentCommand"];

                }


            }
            else
            {
                System.Windows.Forms.MessageBox.Show("No SentCommand Was given", "Error Voice Attack FO4Plugin ",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error
                        );
            }


            //System.Windows.Forms.MessageBox.Show(strPORT.ToString());
            //System.Windows.Forms.MessageBox.Show(strHOST);
            strMsg = strCommand + ";" + strVariable;
            //System.Windows.Forms.MessageBox.Show("strMsg = " + strMsg);
            String sndClient = SendMsg(IPAddress.Parse(strHOST), strPORT, strMsg);
            textValues["ServerResponse"] = sndClient;
            
        }


        static public string SendMsg(IPAddress ipAddress, int thePort, string themessage)
        {
            byte[] bytes = new byte[1024];
            string result = "";
            // Connect to a remote device.
            
            try
            {
                // Establish the remote endpoint for the socket.
                // This example uses port 11000 on the local computer.
                IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, thePort);

                // Create a TCP/IP  socket.
                Socket sender = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    sender.Connect(remoteEP);

                    // Encode the data string into a byte array.
                    byte[] msg = Encoding.ASCII.GetBytes(themessage);

                    // Send the data through the socket.
                    int bytesSent = sender.Send(msg);

                    // Receive the response from the remote device.
                    int bytesRec = sender.Receive(bytes);
                    result = String.Format("{0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));
                    
                    // Release the socket.
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();
                    return result;

                } // TODO Add more descriptive errors.
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                    result = "Erro1";
                    return result;
                }
                catch (SocketException e)
                {
                    System.Windows.Forms.MessageBox.Show("Co Connection to host sever. Check your IP and Port", "Error Voice Attack FO4Plugin ",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error
                        );

                    result = "Not Connected";
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                    result = "Erro3";
                    return result;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                result = "Erro4";
                return result;
            }

        }



    }

}




