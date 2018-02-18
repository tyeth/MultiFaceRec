//Multiple face detection and recognition in real time
//Using EmguCV cross platform .Net wrapper to the Intel OpenCV image processing library for C#.Net
//Writed by Sergio Andrés Guitérrez Rojas
//"Serg3ant" for the delveloper comunity
// Sergiogut1805@hotmail.com
//Regards from Bucaramanga-Colombia ;)
//
//Also Greetings from Bristol, UK.
//tyethgundry@gmail.com

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.IO;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Automation;
using MinimizeAll;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace MultiFaceRec
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public enum DetectionModeStatusTypes
    {
        OFF,
        ON,
        TRAINING
    }

    public partial class FrmPrincipal : Form
    {
        private DetectionModeStatusTypes _detectionModeStatus;

        // ReSharper disable once InconsistentNaming
        public DetectionModeStatusTypes STATUS
        {
            get => _detectionModeStatus;
            set
            {
                _detectionModeStatus = value;
                UpdateMenu(value);
            }
        }

        private void UpdateMenu(DetectionModeStatusTypes value)
        {
            currentDectectionModeStatusToolStripMenuItem.Text = currentDectectionModeStatusToolStripMenuItem.Tag
                .ToString()
                .Replace("STATUS",
                    STATUS == DetectionModeStatusTypes.OFF ? "OFF" :
                    STATUS == DetectionModeStatusTypes.ON ? "ON" : "Training");
        }

        //Declararation of all variables, vectors and haarcascades
        private Image<Bgr, Byte> _currentFrame;
        private Capture _grabber;

        private HaarCascade _face;

        //HaarCascade _eye;
        MCvFont font = new MCvFont(FONT.CV_FONT_HERSHEY_TRIPLEX, 0.5d, 0.5d);
        Image<Gray, byte> result, TrainedFace = null;
        Image<Gray, byte> gray = null;
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labels = new List<string>();
        List<string> NamePersons = new List<string>();
        int ContTrain, NumLabels, t;
        string name, names = null;
        private string username = "Tyeth";
        private EventHandler frmEventHandler;
        private MongoClient dbClient;
        private IMongoDatabase db;
        private IMongoCollection<FacialCroppedMatch> collection;

        public FrmPrincipal()
        {
            InitializeComponent();
            //Load haarcascades for face detection
            _face = new HaarCascade("haarcascade_frontalface_default.xml");
            //_eye = new HaarCascade("haarcascade_eye.xml");
            try
            {
                LoadSettings();
            }
            catch (Exception e)
            {
                MessageBox.Show(
                    $"Failed to load settings collection ({MongoSettingsCollection}) from {MongoDb} database on mongodb server({MongoUrl}).",
                    "Trained faces load", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            try
            {

                LoadTrainedFacesForStartup();
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString());
                MessageBox.Show(
                    "Nothing in binary database, please add at least a face(Simply train the prototype with the Add Face Button).",
                    "Trained faces load", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            STATUS = DetectionModeStatusTypes.OFF;
            // notifyIcon.ShowBalloonTip(int.MaxValue, "Title", "Ballon Content", ToolTipIcon.Info);
            FacesCounter = trainingImages.Count - 1;
            UpdateCurrentBrowsedImage();
        }

        public IMongoCollection<KeyValuePair<string, string>> settingsCollection;
        public void InitialiseDb()
        {
            dbClient = new MongoClient(new MongoUrl(MongoUrl)); //defaults to using admin database on localhost.
            var dbSettingsNoId = new MongoCollectionSettings()
            {
                AssignIdOnInsert = false,
                WriteEncoding = new UTF8Encoding(false,false),
                ReadPreference = ReadPreference.Primary,
                ReadConcern = ReadConcern.Default,
                ReadEncoding = new UTF8Encoding(false, false)
            };
            var dbSettingsWithId = new MongoCollectionSettings()
            {
                AssignIdOnInsert = true,
                WriteEncoding = new UTF8Encoding(false,false),
                ReadPreference = ReadPreference.Primary,
                ReadConcern = ReadConcern.Default,
                ReadEncoding = new UTF8Encoding(false, false)
            };
            db = dbClient.GetDatabase(MongoDb);
            settingsCollection = db.GetCollection<KeyValuePair<string, string>>(MongoSettingsCollection, dbSettingsNoId);
            collection = db.GetCollection<FacialCroppedMatch>(MongoTrustedCollection, dbSettingsWithId
               );
        }

        private void LoadSettings()
        {
            if (settingsCollection == null) { InitialiseDb(); }

            if (settingsCollection.Count(x => true) == 0)
            {
                if (File.Exists(Application.ExecutablePath + "\\Settings.ini"))
                {
                    LoadSettingsFromFile(Application.ExecutablePath + "\\Settings.ini");
                }
                else
                {
                    throw new MongoException("No settings records/documents found in collection.");
                }
            }
            privacyList.Clear();
           // var list = settingsCollection.FindSync(x => x.Key == "PrivacyList").ToList();


            var filterBuilder = Builders<KeyValuePair<string,string>>.Filter;
            var filter = filterBuilder.Eq<string>("k", "PrivacyList");// & filterBuilder.Exists("isTransformed", false);

            var projection = Builders<KeyValuePair<string, string>>.Projection
                    .Exclude("_id")
                .Include("k")
                .Include("v")
                //.Include("client")
                //.Include("url")
                //.Include("fileName")
                //.Include("context")
                ;

            var list= settingsCollection.Find (  filter).Project(  projection  ).ToList();//.ToList<KeyValuePair<string,string>>();///*.Project(projection)*/.FirstOrDefault();
            
                list.ForEach(x =>
                {
                    var xDoc = BsonSerializer.Deserialize<KeyValuePair<string,string>>(x);
                    privacyList.Add(xDoc.Value);
                });
        }

        private void LoadSettingsFromFile(string filePath)
        {
            throw new NotImplementedException();
        }

        private void LoadTrainedFacesForStartup()
        {
            if (collection == null) InitialiseDb();
            //if (collection.Count(x => true) == 0) throw new DataException("The Database has no records or the connection is broken.");
            // labels.Clear();
            long cnt = collection.Count(x => true);
            cnt++;
            Console.WriteLine($"Collection contains {cnt} records.");

            // trainingImages.Clear();
            var list = collection.Find(x => true).ToList();
            //*.Find(y => y.Name.EndsWith(".bmp"))*/.ToList();
            foreach (var doc in list)
            {
                try
                {

                    var ms = new MemoryStream();
                    ms.Write(doc.ImageBytes, 0, doc.ImageBytes.Length);
                    ms.Flush();
                    ms.Seek(0, SeekOrigin.Begin);

                    var bmp = new Bitmap(ms);
                    trainingImages.Add(new Image<Gray, byte>(bmp));

                    labels.Add(doc.Person);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Error with " + doc.Id + " " + e.Message + Environment.NewLine + e.InnerException?.Message);
                }
            }

            if (trainingImages.Count == 0)
                throw new DataException("The Database has no records or the connection is broken.");

            ContTrain = labels.Count;

            ////Load of previus trainned faces and labels for each image
            //string Labelsinfo = File.ReadAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt");
            //string[] Labels = Labelsinfo.Split('%');
            //NumLabels = Convert.ToInt16(Labels[0]);
            //ContTrain = NumLabels;
            //string LoadFaces;

            //for (int tf = 1; tf < NumLabels + 1; tf++)
            //{
            //    LoadFaces = "face" + tf + ".bmp";
            //    trainingImages.Add(new Image<Gray, byte>(Application.StartupPath + "/TrainedFaces/" + LoadFaces));
            //    labels.Add(Labels[tf]);
            //}
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(TxtUsername.Text))
            {
                MessageBox.Show("Enter a name to store the images under first, then click this button again");
                return;
            }

            //Initialize the capture device
            _grabber = new Capture();
            _grabber.QueryFrame();
            //Initialize the FrameGraber event
            frmEventHandler = new EventHandler(FrameGrabber);
            Application.Idle += frmEventHandler;
            button1.Enabled = false;
        }


        private void BtnSaveFoundFace_Click(object sender, System.EventArgs e)
        {
            try
            {
                //Trained face counter
                ContTrain = ContTrain + 1;

                //Get a gray frame from capture device
                gray = _grabber.QueryGrayFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

                //Face Detector
                MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(
                    _face,
                    1.2,
                    10,
                    Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                    new Size(20, 20));

                //Action for each element detected
                foreach (MCvAvgComp f in facesDetected[0])
                {
                    TrainedFace = _currentFrame.Copy(f.rect).Convert<Gray, byte>();
                    break;
                }

                //resize face detected image for force to compare the same size with the 
                //test image with cubic interpolation type method
                TrainedFace = result.Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                trainingImages.Add(TrainedFace);
                labels.Add(TxtUsername.Text);

                //Show face added in gray scale
                imageBox1.Image = TrainedFace;
                PersistNewFace(TrainedFace, TxtUsername.Text);

                this.MessageBoxCheck(TxtUsername.Text + "´s face detected and added :)", "Training OK");

                //MessageBox.Show(TxtUsername.Text + "´s face detected and added :)", "Training OK", MessageBoxButtons.OK,
                //    MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("Enable the face detection first", "Training Fail", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            UpdateCurrentBrowsedImage();

        }

        private async void PersistNewFace(Image<Gray, byte> trainedFace, string text)
        {
            if (collection == null) InitialiseDb();
            var ms = new MemoryStream();
            trainedFace.Bitmap.Save(ms, ImageFormat.Bmp);
            await ms.FlushAsync();
            ms.Seek(0, SeekOrigin.Begin);
            await collection.InsertOneAsync(new FacialCroppedMatch()
            {
                ImageBytes = ms.ToArray(),
                Name = Application.StartupPath + "/TrainedFaces/face" + (trainingImages.Count) + ".bmp",
                Person = text
            });
            ////Write the number of triained faces in a file text for further load
            //File.WriteAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt",
            //    trainingImages.ToArray().Length.ToString() + "%");

            ////Write the labels of triained faces in a file text for further load
            //for (int i = 1; i < trainingImages.ToArray().Length + 1; i++)
            //{
            //    trainingImages.ToArray()[i - 1].Save(Application.StartupPath + "/TrainedFaces/face" + i + ".bmp");
            //    File.AppendAllText(Application.StartupPath + "/TrainedFaces/TrainedLabels.txt",
            //        labels.ToArray()[i - 1] + "%");
            //}
        }

        void FrameGrabber(object sender, EventArgs e)
        {
            label3.Text = "0";
            //label4.Text = "";
            NamePersons.Add("");


            //Get the current frame form capture device
            _currentFrame = _grabber.QueryFrame().Resize(320, 240, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);

            //Convert it to Grayscale
            gray = _currentFrame.Convert<Gray, Byte>();

            //Face Detector
            MCvAvgComp[][] facesDetected = gray.DetectHaarCascade(
                _face,
                1.2,
                10,
                Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                new Size(20, 20));

            //Action for each element detected
            foreach (MCvAvgComp f in facesDetected[0])
            {
                t = t + 1;
                result = _currentFrame.Copy(f.rect).Convert<Gray, byte>()
                    .Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                //draw the face detected in the 0th (gray) channel with blue color
                _currentFrame.Draw(f.rect, new Bgr(Color.Red), 2);


                if (trainingImages.ToArray().Length != 0)
                {
                    //TermCriteria for face recognition with numbers of trained images like maxIteration
                    MCvTermCriteria termCrit = new MCvTermCriteria(ContTrain, 0.001);

                    //Eigen face recognizer
                    EigenObjectRecognizer recognizer = new EigenObjectRecognizer(
                        trainingImages.ToArray(),
                        labels.ToArray(),
                        3000,
                        ref termCrit);

                    name = recognizer.Recognize(result);

                    //Draw the label for each face detected and recognized
                    _currentFrame.Draw(name, ref font, new Point(f.rect.X - 2, f.rect.Y - 2),
                        new Bgr(Color.LightGreen));
                }

                NamePersons[t - 1] = name;
                NamePersons.Add("");


                //Set the number of faces detected on the scene
                label3.Text = facesDetected[0].Length.ToString();


                ////                Set the region of interest on the faces


                //                gray.ROI = f.rect;
                //                MCvAvgComp[][] eyesDetected = gray.DetectHaarCascade(
                //                   _eye,
                //                   1.1,
                //                   10,
                //                   Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING,
                //                   new Size(20, 20));
                //                gray.ROI = Rectangle.Empty;

                //                foreach (MCvAvgComp ey in eyesDetected[0])
                //                {
                //                    Rectangle eyeRect = ey.rect;
                //                    eyeRect.Offset(f.rect.X, f.rect.Y);
                //                    _currentFrame.Draw(eyeRect, new Bgr(Color.Blue), 2);
                //                }
            }

            t = 0;
            var count = 0;
            //Names concatenation of persons recognized
            for (int nnn = 0; nnn < facesDetected[0].Length; nnn++)
            {
                names = names + NamePersons[nnn] + ", ";
                count++;
            }

            if (count > 1)
            {
                RegisterPryingEyes();
            }
            else if ((count == 1 && names.Replace(", ", "") != TxtUsername.Text))
            {
                RegisterUnrecognisedUser();
            }


            {
                //Show the faces procesed and recognized
                imageBoxFrameGrabber.Image = _currentFrame;
                label4.Text = names;
                names = "";
                //Clear the list(vector) of names
                NamePersons.Clear();
            }
        }


        //
        //        public static AutomationElement GetEdgeCommandsWindow(AutomationElement edgeWindow)
        //        {
        //            return edgeWindow.FindFirst(TreeScope.Children, new AndCondition(
        //                new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window),
        //                new PropertyCondition(AutomationElement.NameProperty, "Microsoft Edge")));
        //        }
        //
        //        public static string GetEdgeUrl(AutomationElement edgeCommandsWindow)
        //        {
        //            var adressEditBox = edgeCommandsWindow.FindFirst(TreeScope.Children,
        //                new PropertyCondition(AutomationElement.AutomationIdProperty, "addressEditBox"));
        //
        //            return ((TextPattern) adressEditBox.GetCurrentPattern(TextPattern.Pattern)).DocumentRange.GetText(
        //                int.MaxValue);
        //        }
        //
        //        public static string GetEdgeTitle(AutomationElement edgeWindow)
        //        {
        //            var adressEditBox = edgeWindow.FindFirst(TreeScope.Children,
        //                new PropertyCondition(AutomationElement.AutomationIdProperty, "TitleBar"));
        //
        //            return adressEditBox.Current.Name;
        //        }




        //public delegate bool Win32Callback(IntPtr hwnd, IntPtr lParam);

        //[DllImport("user32.Dll")]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //public static extern bool EnumChildWindows(IntPtr parentHandle, Win32Callback callback, IntPtr lParam);

        ///// <summary>
        ///// Find a child window that matches a set of conditions specified as a Predicate that receives hWnd.  Returns IntPtr.Zero
        ///// if the target window not found.  Typical search criteria would be some combination of window attributes such as
        ///// ClassName, Title, etc., all of which can be obtained using API functions you will find on pinvoke.net
        ///// </summary>
        ///// <remarks>
        /////     <para>Example: Find a window with specific title (use Regex.IsMatch for more sophisticated search)</para>
        /////     <code lang="C#"><![CDATA[var foundHandle = Win32.FindWindow(IntPtr.Zero, ptr => Win32.GetWindowText(ptr) == "Dashboard");]]></code>
        ///// </remarks>
        ///// <param name="parentHandle">Handle to window at the start of the chain.  Passing IntPtr.Zero gives you the top level
        ///// window for the current process.  To get windows for other processes, do something similar for the FindWindow
        ///// API.</param>
        ///// <param name="target">Predicate that takes an hWnd as an IntPtr parameter, and returns True if the window matches.  The
        ///// first match is returned, and no further windows are scanned.</param>
        ///// <returns> hWnd of the first found window, or IntPtr.Zero on failure </returns>
        //public static IntPtr FindWindow(IntPtr parentHandle, Predicate<IntPtr> target)
        //{
        //    var result = IntPtr.Zero;
        //    if (parentHandle == IntPtr.Zero)
        //        parentHandle = Process.GetCurrentProcess().MainWindowHandle;
        //    EnumChildWindows(parentHandle, (hwnd, param) => {
        //        if (target(hwnd))
        //        {
        //            result = hwnd;
        //            return false;
        //        }
        //        return true;
        //    }, IntPtr.Zero);
        //    return result;
        //}


        /// <summary>
        /// return windows text
        /// </summary>
        /// <param name="hWnd"></param>
        /// <param name="lpWindowText"></param>
        /// <param name="nMaxCount"></param>
        /// <returns></returns>
        [DllImport("user32.dll", EntryPoint = "GetWindowText",
            ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpWindowText, int nMaxCount);

        public static string GetWindowTextDirectly(IntPtr h, StringBuilder sb = null, int cnt = 1000)
        {
            if (sb == null) sb = new StringBuilder(1000);
            GetWindowText(h, sb, cnt);
            return sb.ToString();
        }

        const int SW_HIDE = 0;
        const int SW_SHOWNORMAL = 1;
        const int SW_NORMAL = 1;
        const int SW_SHOWMINIMIZED = 2;
        const int SW_SHOWMAXIMIZED = 3;
        const int SW_MAXIMIZE = 3;
        const int SW_SHOWNOACTIVATE = 4;
        const int SW_SHOW = 5;
        const int SW_MINIMIZE = 6;
        const int SW_SHOWMINNOACTIVE = 7;
        const int SW_SHOWNA = 8;
        const int SW_RESTORE = 9;
        const int SW_SHOWDEFAULT = 10;
        const int SW_FORCEMINIMIZE = 11;
        const int SW_MAX = 11;



        public static void MinimizeOutlook(IntPtr hwnd)
        {
            var list = GetChildWindows(hwnd);
            for (int i = 0; i < list.Count; i++)
            {
                var VARIABLE = list[i];
                var t = GetWindowTextDirectly(VARIABLE);
                Debug.WriteLine($"({VARIABLE.ToInt32()}){t}");
                if (string.IsNullOrWhiteSpace(t)) continue;
                ShowWindow(VARIABLE.ToInt32(), SW_MINIMIZE);
            }

            ShowWindow(hwnd.ToInt32(), SW_MINIMIZE);

        }

        [DllImport("User32")]
        private static extern int ShowWindow(int hwnd, int nCmdShow);

        [DllImport("user32")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr window, EnumWindowProc callback, IntPtr i);

        /// <summary>
        /// Returns a list of child windows
        /// </summary>
        /// <param name="parent">Parent of the windows to return</param>
        /// <returns>List of child windows</returns>
        public static List<IntPtr> GetChildWindows(IntPtr parent)
        {
            List<IntPtr> result = new List<IntPtr>();
            GCHandle listHandle = GCHandle.Alloc(result);
            try
            {
                EnumWindowProc childProc = new EnumWindowProc(EnumWindow);
                EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }
            return result;
        }

        /// <summary>
        /// Callback method to be used when enumerating windows.
        /// </summary>
        /// <param name="handle">Handle of the next window</param>
        /// <param name="pointer">Pointer to a GCHandle that holds a reference to the list to fill</param>
        /// <returns>True to continue the enumeration, false to bail</returns>
        private static bool EnumWindow(IntPtr handle, IntPtr pointer)
        {
            GCHandle gch = GCHandle.FromIntPtr(pointer);
            List<IntPtr> list = gch.Target as List<IntPtr>;
            if (list == null)
            {
                throw new InvalidCastException("GCHandle Target could not be cast as List<IntPtr>");
            }
            list.Add(handle);
            //  You can modify this to check to see if you want to cancel the operation, then return a null here
            return true;
        }

        /// <summary>
        /// Delegate for the EnumChildWindows method
        /// </summary>
        /// <param name="hWnd">Window handle</param>
        /// <param name="parameter">Caller-defined variable; we use it for a pointer to our list</param>
        /// <returns>True to continue enumerating, false to bail.</returns>
        public delegate bool EnumWindowProc(IntPtr hWnd, IntPtr parameter);

        /// <summary>
        /// /////////////////////////////////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static AutomationElement GetEdgeCommandsWindow(AutomationElement edgeWindow)
        {
            return edgeWindow.FindFirst(TreeScope.Children, new AndCondition(
                new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window),
                new PropertyCondition(AutomationElement.NameProperty, "Microsoft Edge")));
        }

        public static string GetEdgeUrl(AutomationElement edgeCommandsWindow)
        {
            var adressEditBox = edgeCommandsWindow.FindFirst(TreeScope.Children,
                new PropertyCondition(AutomationElement.AutomationIdProperty, "addressEditBox"));

            return ((TextPattern)adressEditBox.GetCurrentPattern(TextPattern.Pattern)).DocumentRange.GetText(
                int.MaxValue);
        }

        private void FrmPrincipal_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
                notifyIcon.Text =
                    notifyIcon.Text.Substring(0, notifyIcon.Text.Length + 10 >= 64 ? 53 : notifyIcon.Text.Length) +
                    " MINIMIZED";
            }
        }

        private void notifyIcon_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
            if (notifyIcon.Text.EndsWith("MINIMIZED"))
                notifyIcon.Text = notifyIcon.Text.Substring(0, notifyIcon.Text.Length - " MINIMIZED".Length);
        }

        private void notifyIcon_BalloonTipClicked(object sender, EventArgs e)
        {
            Debug.Write("********************************************CLikedD");
        }

        private void notifyIcon_BalloonTipClosed(object sender, EventArgs e)
        {
            Debug.Write("********************************************CLOSED");
        }


        private int FacesCounter = 0;

        public Image<Gray, byte> CurrentImage
        {
            get { return trainingImages[FacesCounter]; }
        }

        public void GetNextPhoto()
        {
            FacesCounter--;
            if (FacesCounter == -1) FacesCounter = trainingImages.Count - 1;
            UpdateCurrentBrowsedImage();
        }

        public void GetPreviousPhoto()
        {
            FacesCounter++;
            if (FacesCounter == trainingImages.Count) FacesCounter = 0;
            UpdateCurrentBrowsedImage();
        }

        public void UpdateCurrentBrowsedImage()
        {
            try
            {
                txtCurrentPicture.Text = $"{(FacesCounter + 1)}/{trainingImages.Count} {labels[FacesCounter]}";
                pictureBox1.Image = CurrentImage.Bitmap;
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                pictureBox1.Refresh();
            }
            catch (Exception e)
            {
            }
        }

        private void RegisterUnrecognisedUser()
        {
            int time = 2000;
            try
            {
                if (STATUS == DetectionModeStatusTypes.ON)
                {
                    Application.Idle -= frmEventHandler;
                    _grabber.Dispose();
                    button1.Enabled = true;
                    //TRIGGER ALARM OR MINIMIZE ALL OR START SCREEN LOCK TIMER
                }
                else if (STATUS == DetectionModeStatusTypes.TRAINING)
                {
                    BtnSaveFoundFace_Click(new { }, new EventArgs { });

                    time = 100;
                }
            }
            catch (Exception e)
            {
            }

            notifyIcon.ShowBalloonTip(time, "Unrecognised Face!", $"The current face on screen is not {username}",
                ToolTipIcon.Error);
        }

        private void RegisterPryingEyes()
        {
            try
            {
                if (STATUS == DetectionModeStatusTypes.ON)
                {
                    Application.Idle -= frmEventHandler;
                    _grabber.Dispose();
                    button1.Enabled = true;
                }
            }
            catch (Exception e)
            {
            }

            notifyIcon.ShowBalloonTip(int.MaxValue, "Unwelcome Face?",
                $"Watchout {username}, There's Prying Eyes About", ToolTipIcon.Warning);
        }

        private void turnONDetectionModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            STATUS = DetectionModeStatusTypes.ON;
        }

        private void turnOFFDetectionModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            STATUS = DetectionModeStatusTypes.OFF;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            username = TxtUsername.Text;
        }

        private void setDetectionModeToTrainingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            STATUS = DetectionModeStatusTypes.TRAINING;
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            GetPreviousPhoto();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            GetNextPhoto();
        }

        public static string GetEdgeTitle(AutomationElement edgeWindow)
        {
            var adressEditBox = edgeWindow.FindFirst(TreeScope.Children,
                new PropertyCondition(AutomationElement.AutomationIdProperty, "TitleBar"));

            return adressEditBox.Current.Name;
        }

        private frmSettings _frm;

        private frmSettings myFrmSettings
        {
            get
            {
                if(_frm==null || _frm.IsDisposed)
                    _frm = new frmSettings();
                    
                return _frm;
            }
            set { _frm = value; }
        }

        public string MongoUrl = "mongodb://localhost:27017";
        public string MongoDb = "faces";
        public string MongoSettingsCollection="settings";
        public string MongoTrustedCollection ="trustedGrey";
        public string MongoScannedCollection ="scanned";
        public string MongoVillainsCollection = "villains";

        private void btnSettings_Click(object sender, EventArgs e)
        {
            var f = myFrmSettings;
            f._senderFrm = this;
            f.Show();
        }

        [DllImport("user32")]
        public static extern IntPtr GetDesktopWindow();

        /// <summary>
        /// //////////////////
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {

            var strOutput = GetRunningProcesses();

            this.runningApps.Text = strOutput;
            //Process.Start("Donate.html");
        }

        public  List<string> privacyList = new List<string>()
        {
            "outlook",
            "microsoftedgecp" ,
            "microsoftedge",
            "edge",
            "firefox",
            "chrome",
            "winword",
            "msteams",
            "teams",
            "skype",
            "hangouts",
            "thunderbird","eudora",
            "mail",
            "hxoutlook"
        };

        private  string GetRunningProcesses()
        {
            const string PROCS = "Running Processes...";
            var sb = new StringBuilder(PROCS);



            // if(Process.GetProcesses().Select(x => x).Any(process => privacyList.Contains(process.ProcessName) )) MinimizeAll.Minimizer.MinimizeAll();
            Process.GetProcesses().Select(x => x).ToList().ForEach(process =>
            {
                var x = process.ProcessName;
                if (sb.Length != PROCS.Length)
                {
                    sb.Append(",\r\n " + x);
                }
                else
                {
                    sb.Append("\r\n " + x);
                }

                if (process.MainWindowHandle != IntPtr.Zero)
                {
                    switch (x.ToLowerInvariant())
                    {
                        case "outlook":
                            MinimizeOutlook(process.MainWindowHandle);
                            break;

                        case "microsoftedgecp":
                        case "microsoftedge":
                        case "edge":
                            AutomationElement main = AutomationElement.FromHandle(GetDesktopWindow());
                            foreach (AutomationElement child in main.FindAll(TreeScope.Children,
                                PropertyCondition.TrueCondition))
                            {
                                AutomationElement window = GetEdgeCommandsWindow(child);
                                if (window == null) // not edge
                                    continue;

                                try
                                {
                                    string url = GetEdgeUrl(window);
                                    string str = GetEdgeTitle(window);
                                    sb.Append("\r\nTab: '" + str + "' (" + url + ")");
                                }
                                catch
                                {
                                }
                            }
                            //                            AutomationElement rooteElement = AutomationElement.FromHandle(process.MainWindowHandle
                            //                            );
                            //
                            //                            // loop through all the tabs and get the names which is the page title 
                            //                            Condition condTabIteme = new PropertyCondition(AutomationElement.ControlTypeProperty,
                            //                                ControlType.Document);
                            //                            foreach (AutomationElement tabitem in rooteElement.FindAll(TreeScope.Children, condTabIteme))
                            //                            {
                            //                                sb.Append("\r\nTab: '" + tabitem.Current.Name + "'");
                            //                            }

                            break;

                        //                            DELIBERATELY DONT SUPPORT IEXPLORE, PPL SHOULD PHASE IT OUT.
                        //                            
                        //                        case "iexplore":
                        //                            AutomationElement rootIeElement = AutomationElement.FromHandle(process.MainWindowHandle
                        //                            );
                        //                            
                        //                            // loop through all the tabs and get the names which is the page title 
                        //                            Condition condTabItemIe = new PropertyCondition(AutomationElement.ControlTypeProperty,
                        //                                ControlType.TabItem);
                        //                            foreach (AutomationElement tabitem in rootIeElement.FindAll(TreeScope.Children, condTabItemIe))
                        //                            {
                        //                                sb.Append("\r\nTab: '" + tabitem.Current.Name + "'");
                        //                            }
                        //
                        //                            break;
                        case "firefox":
                            AutomationElement rootElement = AutomationElement.FromHandle(process.MainWindowHandle);
                            Condition condDocAll = new PropertyCondition(AutomationElement.ControlTypeProperty,
                                ControlType.Document);
                            foreach (AutomationElement docElement in rootElement.FindAll(TreeScope.Descendants,
                                condDocAll))
                            {
                                foreach (AutomationPattern pattern in docElement.GetSupportedPatterns())
                                {
                                    if (docElement.GetCurrentPattern(pattern) is ValuePattern)
                                        sb.Append(Environment.NewLine + "Tab '" +
                                                  docElement.Current.Name
                                                      .ToString()
                                                  + "' (" +
                                                  (docElement.GetCurrentPattern(pattern) as ValuePattern).Current.Value
                                                  .ToString() + ")"
                                        );
                                }
                            }

                            break;
                        case "chrome":


                            //                    AutomationElement root = AutomationElement.FromHandle(process.MainWindowHandle);
                            //                    Condition condition = new PropertyCondition(AutomationElement.ControlTypeProperty, ControlType.Window);
                            //                    var tabs = root.FindAll(TreeScope.Descendants, condition);
                            //                    for (int i = 0; i < tabs.Count; i++)
                            //                    {
                            //                        var tabitem = tabs[i];
                            AutomationElement root = AutomationElement.FromHandle(process.MainWindowHandle);
                            Condition condNewTab = new PropertyCondition(AutomationElement.NameProperty, "New Tab");
                            AutomationElement elmNewTab = root.FindFirst(TreeScope.Descendants, condNewTab);
                            // get the tabstrip by getting the parent of the 'new tab' button 
                            TreeWalker treewalker = TreeWalker.ControlViewWalker;
                            AutomationElement elmTabStrip = treewalker.GetParent(elmNewTab);
                            // loop through all the tabs and get the names which is the page title 
                            Condition condTabItem = new PropertyCondition(AutomationElement.ControlTypeProperty,
                                ControlType.TabItem);
                            foreach (AutomationElement tabitem in elmTabStrip.FindAll(TreeScope.Children, condTabItem))
                            {
                                sb.Append("\r\nTab: '" + tabitem.Current.Name + "'");
                            }


                            break;
                        default:
                            break;
                    }
                }
            });
            var strOutput = sb.ToString();
            return strOutput;
        }
    }
}