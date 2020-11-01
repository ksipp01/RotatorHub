//tabs=4
// --------------------------------------------------------------------------------
// TODO fill in this information for your driver, then remove this line!
//
// ASCOM Rotator driver for scopefocusServer
//
// Description:	Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam 
//				nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam 
//				erat, sed diam voluptua. At vero eos et accusam et justo duo 
//				dolores et ea rebum. Stet clita kasd gubergren, no sea takimata 
//				sanctus est Lorem ipsum dolor sit amet.
//
// Implements:	ASCOM Rotator interface version: <To be completed by driver developer>
// Author:		(XXX) Your N. Here <your@email.here>
//
// Edit Log:
//
// Date			Who	Vers	Description
// -----------	---	-----	-------------------------------------------------------
// dd-mmm-yyyy	XXX	6.0.0	Initial edit, created from ASCOM driver template
// --------------------------------------------------------------------------------
//


// This is used to define code in the template that is specific to one class implementation
// unused code canbe deleted and this definition removed.
#define Rotator

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Runtime.InteropServices;

using ASCOM;
using ASCOM.Astrometry;
using ASCOM.Astrometry.AstroUtils;
using ASCOM.Utilities;
using ASCOM.DeviceInterface;
using System.Globalization;
using System.Collections;

namespace ASCOM.scopefocusServer
{
    //
    // Your driver's DeviceID is ASCOM.scopefocusServer.Rotator
    //
    // The Guid attribute sets the CLSID for ASCOM.scopefocusServer.Rotator
    // The ClassInterface/None addribute prevents an empty interface called
    // _scopefocusServer from being created and used as the [default] interface
    //
    // TODO Replace the not implemented exceptions with code to implement the function or
    // throw the appropriate ASCOM exception.
    //

    /// <summary>
    /// ASCOM Rotator Driver for scopefocusServer.
    /// </summary>
    [Guid("750c8652-30c7-427a-bbf4-e36ebbccd125")]
    [ProgId("ASCOM.scopefocusServer.Rotator")]
    [ServedClassName("ASCOM Rotator Driver for scopefocusServer")]
    [ClassInterface(ClassInterfaceType.None)]
    public class Rotator : ReferenceCountedObjectBase, IRotatorV2
    {

        //****** add
        //  private Config config = new Config();
        private Serial serialPort;


        //  private TextWriter log;


            // remd 4-24-17
   //     System.Threading.Mutex mutex = new System.Threading.Mutex();


        //float lastPos = 0;
        ////    double lastTemp = 0;
        //bool lastMoving = false;
        //bool lastLink = false;

        //long UPDATETICKS = (long)(1 * 10000000.0); // 10,000,000 ticks in 1 second
        //long lastUpdate = 0;


        //long lastL = 0;
        //************end add
        // end rem

        /// <summary>
        /// ASCOM DeviceID (COM ProgID) for this driver.
        /// The DeviceID is used by ASCOM applications to load the driver at runtime.
        /// </summary>
        /// 
        private static short interfaceVersion = 2;
        internal static string driverID = "ASCOM.ScopefocusServer.Rotator";
        private static string driverDescription = "ASCOM Rotator Driver for scopefocusServer";
        //    internal static string driverID = "ASCOM.scopefocusServer.Rotator";




        // TODO Change the descriptive string for your driver then remove this line
        /// <summary>
        /// Driver description that displays in the ASCOM Chooser.
        /// </summary>
        /// 
        private static string driverShortName = "scopefocusServer Rotator";

          // private static string driverDescription = "ASCOM Rotator Driver for scopefocusServer.";

        internal static string comPortProfileName = "COM Port"; // Constants used for Profile persistence
        internal static string comPortDefault = "COM1";
        internal static string traceStateProfileName = "Trace Level";
        internal static string traceStateDefault = "false";

        internal static string comPort; // Variables to hold the currrent device configuration

        internal static bool traceState;
     //   internal static int stepsPerDegree;

        /// <summary>
        /// Private variable to hold the connected state
        /// </summary>
        /// // 4-27-17
        private bool connectedState;

        /// <summary>
        /// Private variable to hold an ASCOM Utilities object
        /// </summary>
        private Util utilities;

        /// <summary>
        /// Private variable to hold an ASCOM AstroUtilities object to provide the Range method
        /// </summary>
        private AstroUtils astroUtilities;

        /// <summary>
        /// Variable to hold the trace logger object (creates a diagnostic log file with information that you specify)
        /// </summary>
        internal static TraceLogger tl;

        /// <summary>
        /// Initializes a new instance of the <see cref="scopefocusServer"/> class.
        /// Must be public for COM registration.
        /// </summary>
        /// 
       

        public Rotator()
        {

        //    SharedResources.tl.LogMessage(driverShortName, "Starting initialization");
         //   connectedState = false; // Initialise connected to false
        //    SharedResources.tl.LogMessage(driverShortName, "Completed initialization");

            driverID = Marshal.GenerateProgIdForType(this.GetType());

            // 4-27-17
            connectedState = false;




            
     //      SharedResources.tl = new TraceLogger("", "scopefocusServer");

          //  remd 4-24-17
        //    ReadProfile(); // Read device configuration from the ASCOM Profile store



         // added 10-30-2020
        //    tl.LogMessage("Rotator", "Starting initialisation");

            //connectedState = false; // Initialise connected to false
            //utilities = new Util(); //Initialise util object
            //astroUtilities = new AstroUtils(); // Initialise astro utilities object
            ////TODO: Implement your additional construction here

       //     tl.LogMessage("Rotator", "Completed initialisation");







        }


        //
        // PUBLIC COM INTERFACE IRotatorV2 IMPLEMENTATION
        //

        #region Common properties and methods.

        /// <summary>
        /// Displays the Setup Dialog form.
        /// If the user clicks the OK button to dismiss the form, then
        /// the new settings are saved, otherwise the old values are reloaded.
        /// THIS IS THE ONLY PLACE WHERE SHOWING USER INTERFACE IS ALLOWED!
        /// </summary>
        public void SetupDialog()
        {
            SharedResources.RunSetupDialog();


            // consider only showing the setup dialog if not connected
            // or call a different dialog if connected

            //if (SharedResources.SharedSerial.Connected)
            //{
            //   System.Windows.Forms.MessageBox.Show("Already connected, just press OK");
            //}
            //else
            //{

                //using (SetupDialogForm F = new SetupDialogForm())
                //{
                //    var result = F.ShowDialog();
                //    if (result == System.Windows.Forms.DialogResult.OK)
                //    {
                //        WriteProfile(); // Persist device configuration values to the ASCOM Profile store
                //    }
                //}


                //using (ServerSetupDialog setupSerial = new ServerSetupDialog())
                //{
                //    setupSerial.ShowDialog();
                //}
         //   }



            // remd 4-24-17
            //if (IsConnected)
            //    System.Windows.Forms.MessageBox.Show("Already connected, just press OK");


        }

        public ArrayList SupportedActions
        { 
            // To do
            get
            {
               SharedResources.tl.LogMessage("SupportedActions Get", "Returning empty arraylist");
                return new ArrayList();
            }
        }

        public string Action(string actionName, string actionParameters)
        {
           //LogMessage("", "Action {0}, parameters {1} not implemented", actionName, actionParameters);
            throw new ASCOM.ActionNotImplementedException("Action " + actionName + " is not implemented by this driver");
        }

        public void CommandBlind(string command, bool raw)
        {
            CheckConnected("CommandBlind");
            // Call CommandString and return as soon as it finishes
            this.CommandString(command, raw);
            // or
            throw new ASCOM.MethodNotImplementedException("CommandBlind");
            // DO NOT have both these sections!  One or the other
        }

        public bool CommandBool(string command, bool raw)
        {
            CheckConnected("CommandBool");
            string ret = CommandString(command, raw);
            // TODO decode the return string and return true or false
            // or
            throw new ASCOM.MethodNotImplementedException("CommandBool");
            // DO NOT have both these sections!  One or the other
        }
   //     private string ASCOMfunction = "f"; // may not be needed
        public string CommandString(string command, bool raw)
        {
            CheckConnected("CommandString");
            // it's a good idea to put all the low level communication with the device here,
            // then all communication calls this function
            // you need something to ensure that only one command is in progress at a time

            // throw new ASCOM.MethodNotImplementedException("CommandString");
          //  return SharedResources.rawCommand(ASCOMfunction, command, raw); //? ascomfunction ???? maynot be needed.....
            return SharedResources.rawCommand(command, raw); //? ascomfunction ???? maynot be needed.....

        }

        //  **************** move this below to shared resourses



        //    if (!this.Connected)
        //    {
        //        throw new ASCOM.NotConnectedException();

        //    }

        //    string temp = "999";
        //    mutex.WaitOne();
        //    try
        //    {
        //        tl.LogMessage("Sending Command: ", command);
        //        if (!command.EndsWith("#"))
        //            command += "#";


        //        serialPort.ClearBuffers();


        //        serialPort.Transmit(command);


        //        // get the return value
        //        temp = serialPort.ReceiveTerminated("#");


        //        serialPort.ClearBuffers();


        //        tl.LogMessage("Got Response: ", temp);

        //    }
        //    catch (Exception e)
        //    {
        //        tl.LogMessage("Caught exception in CommandString ", e.Message);

        //    }
        //    finally
        //    {
        //        mutex.ReleaseMutex();
        //    }

        //    return temp;

        //}

        public void Dispose()
        {
            // remd 4-24-17

            //// Clean up the tracelogger and util objects
            //tl.Enabled = false;
            //tl.Dispose();
            //tl = null;
            //utilities.Dispose();
            //utilities = null;
            //astroUtilities.Dispose();
            //astroUtilities = null;


          //  remd 4-24-17

            ////**** added
            //if (serialPort == null)
            //    return;
            //serialPort.Connected = false;
            //serialPort.Dispose();
            //serialPort = null;
            ////**** end addt


        }

        public bool Connected
        {
            // 4-27-17 mods
            get { return IsConnected; }
            set
            {
                {
                    SharedResources.tl.LogMessage("Connected Set", value.ToString());
                    if (value == IsConnected)
                        return;
                    if (value)
                    {
                        if (IsConnected) return;
                        //   SharedResources.tl.LogMessage(driverShortName + "Connected Set", "Connecting to port " + scopefocusServer.Properties.Settings.Default.);
                        SharedResources.tl.LogMessage("Connected Set", "Connecting to port " + SharedResources.SharedSerial.PortName);
                        SharedResources.Connected = true;

                        // remd 4-25-17
                        connectedState = SharedResources.Connected;
                        if (connectedState)
                        {
                            //    //focuserMaxStep = MaxIncrement;
                            //    //focuserCurPos = Position;
                            //    //focuserMaxPos = MaxStep;
                        }
                    }
                    else
                    {
                        connectedState = false;
                        SharedResources.Connected = false;
                     //   SharedResources.tl.LogMessage(driverShortName + " Switch Connected Set", "Disconnected, " + SharedResources.connections + " connections left");
                    }
                }
            }
        }


        // prev working version prior to 4-27-17
        //public bool Connected
        //{
        //    // 4-23-17 add
        //    get { return IsConnected; }
        //    set
        //    {
        //        {
        //            //  SharedResources.tl.LogMessage("Connected Set", value.ToString());
        //            if (value == IsConnected)
        //                return;
        //            if (value)
        //            {
        //                if (IsConnected) return;
        //                // SharedResources.tl.LogMessage(driverShortName + "Connected Set", "Connecting to port " + QA.Properties.Settings.Default.COMPort);
        //                //   SharedResources.tl.LogMessage("Connected Set", "Connecting to port " + SharedResources.SharedSerial.PortName);
        //                SharedResources.Connected = true;

        //                // remd 4-25-17
        //                //connectedState = SharedResources.Connected;
        //                //if (connectedState)
        //                //{
        //                //    //focuserMaxStep = MaxIncrement;
        //                //    //focuserCurPos = Position;
        //                //    //focuserMaxPos = MaxStep;
        //                //}
        //            }
        //            else
        //            {
        //                //  connectedState = false;
        //                SharedResources.Connected = false;
        //                //   SharedResources.tl.LogMessage(driverShortName + " Switch Connected Set", "Disconnected, " + SharedResources.connections + " connections left");
        //            }
        //        }
        //    }
        //}




        public string Description
        {
            // TODO customise this device description
            get
            {
               SharedResources.tl.LogMessage("Description Get", driverDescription);
                return driverDescription;
            }
        }

        public string DriverInfo
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                // TODO customise this driver description
                string driverInfo = "Information about the driver itself. Version: " + String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
               SharedResources.tl.LogMessage("DriverInfo Get", driverInfo);
                return driverInfo;
            }
        }

        public string DriverVersion
        {
            get
            {
                Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
                // string driverVersion = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", version.Major, version.Minor);
                string driverVersion = String.Format(CultureInfo.InvariantCulture, "{0}.{1}.{2}.{3}", version.Major, version.Minor, version.Build, version.Revision);
               SharedResources.tl.LogMessage("DriverVersion Get", driverVersion);
                return driverVersion;
            }
        }

        public short InterfaceVersion
        {
            // set by the driver wizard
            get
            {
                SharedResources.tl.LogMessage(driverShortName + " InterfaceVersion Get", interfaceVersion.ToString());

                //LogMessage("InterfaceVersion Get", "2");
                //  return Convert.ToInt16(interfaceVersion);
                //  return SharedResources.InterfaceVersion;
                return interfaceVersion;
            }
        }

        // moved to sharedResources
      //  public short InterfaceVersion
      //  {
            // set by the driver wizard
       //     get
       //     {
       //         SharedResources.tl.LogMessage(driverShortName + " InterfaceVersion Get", interfaceVersion.ToString());

                //LogMessage("InterfaceVersion Get", "2");
         //       return Convert.ToInt16(interfaceVersion);
         //   }
     //   }

        public string Name
        {
            get
            {

                SharedResources.tl.LogMessage(driverShortName + " Name Get", driverShortName);
                return driverShortName;


                // string name = "scopefocusServer";
                //// string name = "Short driver name - please customise";
                //tl.LogMessage("Name Get", name);
                //return name;
            }
        }

        #endregion

        #region IRotator Implementation

        private float rotatorPosition = 0; // Absolute position angle of the rotator 

        public bool CanReverse
        {
            get
            {
                SharedResources.tl.LogMessage("CanReverse Get", false.ToString());
              //  return SharedResources.CanReverse;
                
                return false;
            }
        }

        public void Halt()
        {
            SharedResources.Halt();

          //  CommandString("S#", false);
            //  tl.LogMessage("Halt", "Not implemented");
         //   throw new ASCOM.MethodNotImplementedException("Halt");
        }

        public bool IsMoving
        {
            get
            {
                return SharedResources.IsMoving;
                //DoUpdate();
                //return lastMoving;
                // tl.LogMessage("IsMoving Get", false.ToString()); // This rotator has instantaneous movement
                //  return false;
            }
        }
        //remd 4-24-17
        //public bool Link
        //{
        //    get
        //    {
        //        long now = DateTime.Now.Ticks;
        //        if (now - lastL > UPDATETICKS)
        //        {
        //            if (serialPort != null)
        //                lastLink = serialPort.Connected;

        //            lastL = now;
        //            return lastLink;
        //        }

        //        return lastLink;
        //    }
        //    set
        //    {
        //        this.Connected = value;
        //    }


        //    /*
        //    get
        //    {
        //        tl.LogMessage("Link Get", this.Connected.ToString());
        //        return this.Connected; // Direct function to the connected method, the Link method is just here for backwards compatibility
        //    }
        //    set
        //    {
        //        tl.LogMessage("Link Set", value.ToString());
        //        this.Connected = value; // Direct function to the connected method, the Link method is just here for backwards compatibility
        //    }
        //     */
        //}


        public void Move(float pos)
        {
            SharedResources.Move(pos);

            //double moveTo = StepperPos + RelativeAngleToMotorSteps(pos);  // current position in steps + number of steps needed to 
            //targetPosition = pos;
            //if (moveTo >= 720 * stepsPerDegree) // was 72000
            //    moveTo -= 360 * stepsPerDegree;
            //if (moveTo < 0)
            //    moveTo += 360 * stepsPerDegree;
            //CommandString("M " + Math.Round(moveTo, 0) + "#", false);  // Position was 'int value' for focuser
            //lastMoving = true;  //remd 1-12-15

            //tl.LogMessage("Move", Position.ToString()); // Move by this amount
            //rotatorPosition += Position;
            //rotatorPosition = (float)astroUtilities.Range(rotatorPosition, 0.0, true, 360.0, false); // Ensure value is in the range 0.0..359.9999...
        }


        //public double RelativeAngleToMotorSteps(float angle)
        //{
        //    var targetSteps1 = angle % 360.00F * stepsPerDegree;
        //    return targetSteps1;
        //}
        //public double PositionAngleToMotorSteps(float targetPositionAngle)
        //{
        //    float targetAngle = 0;
        //    var absTargetAngle1 = targetPositionAngle + 360;
        //    var absTargetAngle2 = targetPositionAngle;
        //    var angleDelta1 = absTargetAngle1 - StepperPos / stepsPerDegree;
        //    var angleDelta2 = StepperPos / stepsPerDegree - absTargetAngle2;
        //    if (absTargetAngle1 < 0 || absTargetAngle1 > 720)
        //    {
        //        targetAngle = absTargetAngle2;
        //        return targetAngle * stepsPerDegree;
        //    }
        //    if (absTargetAngle2 < 0 || absTargetAngle2 > 720)
        //    {
        //        targetAngle = absTargetAngle1;
        //        return targetAngle * stepsPerDegree;
        //    }



        //    if (angleDelta1 < angleDelta2)
        //    {
        //        // if target is close to 0 or 72000 AND the move is < 90 degrees then go there(acceptable if close)...otherwise want to stay close to 36000
        //        if ((absTargetAngle1 < 90 && angleDelta1 < 90) || (absTargetAngle1 > 630 && angleDelta1 < 90))
        //            targetAngle = absTargetAngle1;
        //        else
        //            targetAngle = absTargetAngle2;
        //        if (absTargetAngle1 >= 90 && absTargetAngle1 <= 630) // if outside the close to 0/72000 zone then use smaller delta
        //            targetAngle = absTargetAngle1;


        //    }
        //    else // delta 2 is smaller so in general use it unless within 90 of 0/72000 OR if close then ok 
        //    {
        //        if ((absTargetAngle2 < 90 && angleDelta2 < 90) || (absTargetAngle2 > 630 && angleDelta2 < 90))
        //            targetAngle = absTargetAngle2;
        //        else
        //            targetAngle = absTargetAngle1;
        //        if (absTargetAngle2 >= 90 && absTargetAngle2 <= 630)
        //            targetAngle = absTargetAngle2;

        //    }
        //    return targetAngle * stepsPerDegree;
        //}



        public void MoveAbsolute(float pos)
        {
            SharedResources.MoveAbsolute(pos);

            //var stepPosition = PositionAngleToMotorSteps(pos);
            //targetPosition = pos;
            ////   TargetPosition = pos;
            //CommandString("M " + Math.Round(stepPosition, 0) + "#", false);  // Position was 'int value' for focuser  // corrects for 100 steps per degree, need to replace with user defined variable.  
            //lastMoving = true;  //remd 1-12-15


            //tl.LogMessage("MoveAbsolute", Position.ToString()); // Move to this position
            //rotatorPosition = Position;
            //rotatorPosition = (float)astroUtilities.Range(rotatorPosition, 0.0, true, 360.0, false); // Ensure value is in the range 0.0..359.9999...
        }

        public float StepperPos
        {
            get
            {
               return SharedResources.StepperPos;
                //DoUpdate();
                //return lastPos;

            }
        }

   //     private float targetPosition = 0; // Absolute stepper position of the rotator (in steps)  

        public float Position
        {
            get
            {
                return SharedResources.Position;
                //DoUpdate();
                ////rotatorPosition = lastPos;
                ////return lastPos;
                ////   return (lastPos - 9000) / 100 % 360;  // was get{}
                //var pos = (lastPos - 360 * stepsPerDegree) / stepsPerDegree;
                //if (pos < 0)
                //    pos += 360.00F;
                //return pos;

                //tl.LogMessage("Position Get", rotatorPosition.ToString()); // This rotator has instantaneous movement
                //return rotatorPosition;
            }
        }

       

        public bool Reverse
        {
            get
            {
               // return SharedResources.ReverseState;
                SharedResources.tl.LogMessage("Reverse Get", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("Reverse", false);
            }
            set
            {
                //try
                //{
                //    SharedResources.ReverseState = value;
                //}
                //catch (Exception xcp)
                //{
                //    throw xcp;
                //}
               SharedResources.tl.LogMessage("Reverse Set", "Not implemented");
                throw new ASCOM.PropertyNotImplementedException("Reverse", true);
            }
        }

        public float StepSize
        {
            get
            {
                return SharedResources.StepSize;
                //if (stepsPerDegree > 100)
                //    return .01F;  // minimum of 0.01
                //else
                //    return 1F / stepsPerDegree; // since carrying out 3 decimla points doesn't work mult by 10

                //tl.LogMessage("StepSize Get", "Not implemented");
                //throw new ASCOM.PropertyNotImplementedException("StepSize", false);
            }
        }

        public float TargetPosition
        {
            get
            {
                return SharedResources.TargetPosition;
              //  return targetPosition;

                //tl.LogMessage("TargetPosition Get", rotatorPosition.ToString()); // This rotator has instantaneous movement
                //return rotatorPosition;
            }
        }


        //private void DoUpdate()
        //{
        //    // only allow access for "gets" once per second.
        //    // if inside of 1 second the buffered value will be used.
        //    if (DateTime.Now.Ticks > UPDATETICKS + lastUpdate)
        //    {
        //        lastUpdate = DateTime.Now.Ticks;


        //        // focuser returns a string like:
        //        // m:false;s:1000;t:25.20$
        //        //   m - denotes moving or not
        //        //   s - denotes the position in steps
        //        //   t - denotes the temperature, always in C


        //        String val = CommandString("G#", false);


        //        // split the values up.  Ideally you should check for null here.  
        //        // if something goes wrong this will throw an exception...no bueno...


        //        //focuser sends P 200;M true#  for e.g.

        //        String[] vals = val.Replace('#', ' ').Trim().Split(';');

        //        string valTrim = vals[0].Replace('#', ' ');
        //        string pos = valTrim.Replace('P', ' ').Trim();
        //        // these values are used in the "Get" calls.  That way the client gets an immediate
        //        // response.  However it may up to 1 second out of date.
        //        // Thus "lastMoving" must be set to true when the move is initiated in "Move"

        //        lastPos = Convert.ToSingle(pos);  // raw stepper position in 'steps' from 0 (which is -90 degrees) 
        //        //    lastMoving = false;
        //        lastMoving = vals[1].Substring(2) == "true" ? true : false;  //*** remd 1-12-15
        //        //   *** 1-12-15  to implement this need to change arduino code to retrun something liek "M:True" 
        //        //   *** like example above line 640, then slipt ther string into an array and decifer them



        //        //    lastPos = Convert.ToInt16(vals[1].Substring(2));
        //        //    lastTemp = Convert.ToDouble(vals[2].Substring(2));
        //    }
        //}

        #endregion

        #region Private properties and methods
        // here are some useful properties and methods that can be used as required
        // to help with driver development

        //#region ASCOM Registration

        //// Register or unregister driver for ASCOM. This is harmless if already
        //// registered or unregistered. 
        ////
        ///// <summary>
        ///// Register or unregister the driver with the ASCOM Platform.
        ///// This is harmless if the driver is already registered/unregistered.
        ///// </summary>
        ///// <param name="bRegister">If <c>true</c>, registers the driver, otherwise unregisters it.</param>
        //private static void RegUnregASCOM(bool bRegister)
        //{
        //    using (var P = new ASCOM.Utilities.Profile())
        //    {
        //        P.DeviceType = "Rotator";
        //        if (bRegister)
        //        {
        //            P.Register(driverID, driverDescription);
        //        }
        //        else
        //        {
        //            P.Unregister(driverID);
        //        }
        //    }
        //}

        ///// <summary>
        ///// This function registers the driver with the ASCOM Chooser and
        ///// is called automatically whenever this class is registered for COM Interop.
        ///// </summary>
        ///// <param name="t">Type of the class being registered, not used.</param>
        ///// <remarks>
        ///// This method typically runs in two distinct situations:
        ///// <list type="numbered">
        ///// <item>
        ///// In Visual Studio, when the project is successfully built.
        ///// For this to work correctly, the option <c>Register for COM Interop</c>
        ///// must be enabled in the project settings.
        ///// </item>
        ///// <item>During setup, when the installer registers the assembly for COM Interop.</item>
        ///// </list>
        ///// This technique should mean that it is never necessary to manually register a driver with ASCOM.
        ///// </remarks>
        //[ComRegisterFunction]
        //public static void RegisterASCOM(Type t)
        //{
        //    RegUnregASCOM(true);
        //}

        ///// <summary>
        ///// This function unregisters the driver from the ASCOM Chooser and
        ///// is called automatically whenever this class is unregistered from COM Interop.
        ///// </summary>
        ///// <param name="t">Type of the class being registered, not used.</param>
        ///// <remarks>
        ///// This method typically runs in two distinct situations:
        ///// <list type="numbered">
        ///// <item>
        ///// In Visual Studio, when the project is cleaned or prior to rebuilding.
        ///// For this to work correctly, the option <c>Register for COM Interop</c>
        ///// must be enabled in the project settings.
        ///// </item>
        ///// <item>During uninstall, when the installer unregisters the assembly from COM Interop.</item>
        ///// </list>
        ///// This technique should mean that it is never necessary to manually unregister a driver from ASCOM.
        ///// </remarks>
        //[ComUnregisterFunction]
        //public static void UnregisterASCOM(Type t)
        //{
        //    RegUnregASCOM(false);
        //}

        //#endregion

        /// <summary>
        /// Returns true if there is a valid connection to the driver hardware
        /// </summary>
        private bool IsConnected
        {
            get
            {
               // return SharedResources.IsConnected;
                // TODO check that the driver hardware connection exists and is connected to the hardware
                // 4-27-17  was return above sharedResourses....
                return connectedState;
            }
        }

        /// <summary>
        /// Use this function to throw an exception if we aren't connected to the hardware
        /// </summary>
        /// <param name="message"></param>
        private void CheckConnected(string message)
        {
            if (!IsConnected)
            {
                throw new ASCOM.NotConnectedException(message);
            }
        }

        /// <summary>
        /// Read the device configuration from the ASCOM Profile store
        /// </summary>
        /// 

            //remd 4-24-17 moved to serversetupdialog
        //internal void ReadProfile()
        //{
        //    using (Profile driverProfile = new Profile())
        //    {
        //        driverProfile.DeviceType = "Rotator";
        //        tl.Enabled = Convert.ToBoolean(driverProfile.GetValue(driverID, traceStateProfileName, string.Empty, traceStateDefault));
        //        comPort = driverProfile.GetValue(driverID, comPortProfileName, string.Empty, comPortDefault);
        //    }
        //}

        ///// <summary>
        ///// Write the device configuration to the  ASCOM  Profile store
        ///// </summary>
        //internal void WriteProfile()
        //{
        //    using (Profile driverProfile = new Profile())
        //    {
        //        driverProfile.DeviceType = "Rotator";
        //        driverProfile.WriteValue(driverID, traceStateProfileName, tl.Enabled.ToString());
        //        driverProfile.WriteValue(driverID, comPortProfileName, comPort.ToString());
        //    }
        //}

        /// <summary>
        /// Log helper function that takes formatted strings and arguments
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="message"></param>
        /// <param name="args"></param>
        internal static void LogMessage(string identifier, string message, params object[] args)
        {
            var msg = string.Format(message, args);
            tl.LogMessage(identifier, msg);
        }
        #endregion
    }
}
