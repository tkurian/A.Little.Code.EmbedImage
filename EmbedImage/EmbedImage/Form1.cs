/*
 *  File Name:      Form1.cs 
 *  Name:           Tina kurian
 *  Last Modified:  March 24th 2012
 *  Description:    The purpose of this class is to decode and encode a bitmap image
 *  
 */


/**************************************************************
 *                  USING STATEMENTS                          *
 * ************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Collections; 

namespace EmbedImage
{

    /*
     *  Class Name: MyImages
     *  Description: The purpose of this class is to decode and encode a bitmap image. It
     *               will use unsafe code and handle the images at a pixel level using
     *               bit manipulation
     *  
     */
    public partial class MyImages : Form
    {

        /**************************************************************
         *                      PRIVATE VARIABLES                     *
         * ************************************************************/

        /********************** CONSTANT ****************************/
        private const int BLUE = 0;
        private const int GREEN = 1;
        private const int RED = 2;

        /********************** STRING ****************************/
        private string MESSAGE = Convert.ToString(System.DateTime.Now);

        /********************** BYTE ARRAY ****************************/
        private byte[] arrays = null;

        /********************** INTEGER ****************************/
        private int countToByte = 0;
        private int redCount = 0;
        private int greenCount = 0;
        private int blueCount = 0;

        /********************** BYTE ****************************/
        
        /* BYTE MASKS */
        private const byte BlueMask = 248;       // 11111000
        private const byte GreenMask = 252;      // 11111100
        private const byte RedMask = 248;        // 11111000

        /* INVERTED BYTE MASKS */
        private const byte InverseBlueMask = 7;  // 00000111
        private const byte InverseGreenMask = 3; // 00000011
        private const byte InverseRedMask = 7;   // 00000111

        /* VALUE BYTE MASKS (indicates where the colours are located in a byte) */
        private const byte BValueMask = 224;     // 11100000
        private const byte GValueMask = 24;      // 00011000
        private const byte RValueMask = 7;       // 00000111


        /************************* BOOLEAN ****************************/
        private bool isGetMessage = false;
        private bool isEmbedImage = false;
        private bool isLoaded = false;
        private bool isImageEmbedded = false;

        /************************* BITMAP+ ****************************/
        private Bitmap firstLoaded;
        private Bitmap theImage;
        private Bitmap imageEmbedded;

        /********************** Graphics+ ****************************/
        private Graphics graphicsWindow;        // reference to the graphic surface of this window
        private Graphics graphicsImage;         // reference to in-memory surface

        /*************************** List+ ****************************/
        private List<byte> messageBytes = new List<byte>();
        private List<String> finalMessage = new List<String>();

        /********************** BitArray+ ****************************/
        private BitArray bitsOfMessage = new BitArray(8);










        /**************************************************************
         *                        PUBLIC METHODS                      *
         **************************************************************/

        /*
         *  Method Name: private void MyImages()
         *  Method Parameters:n/a
         *  Parameter Description: n/a
         *  Purpose: this is the constructor
         *  
         */
        public MyImages()
        {
            InitializeComponent();
            textBox1.ReadOnly = true; 
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);
        }










        /**************************************************************
         *                        PRIVATE METHODS                     *
         **************************************************************/

        /************************* PAINT METHOD ***********************/

        /*
         *  Method Name: private void MyImages_Paint(object sender, PaintEventArgs e)
         *  Method Parameters: object sender, PaintEventArgs e
         *  Parameter Description: object sender, PaintEventArgs e: handles event args
         *  Purpose: this method is called as soon as the winform required a re-painting
         *  
         */
        private void MyImages_Paint(object sender, PaintEventArgs e)
        {
            if (isLoaded == true)
            {
                theImage = new Bitmap(Width, Height);     // bitmap for window surface copy
                graphicsWindow = e.Graphics;   // get our current window's surface
                graphicsImage = Graphics.FromImage(theImage);     // create surfaces from the bitmaps
                graphicsImage.DrawImage(firstLoaded, 0, 0, Width, Height);

                if (isEmbedImage == true)
                {
                    theImage = embedMessageInImage(theImage);
                    isGetMessage = false;
                }
                else if (isGetMessage == true)
                {
                    getEmbeddedMessage(imageEmbedded);

                }

                if (isGetMessage == false)
                {
                    graphicsWindow.DrawImage(theImage, 0, 0);
                }
                else if (isGetMessage == true)
                {
                    graphicsWindow.DrawImage(imageEmbedded, 0, 0);
                    isGetMessage = false; 

                }
            }
        }









        /*********************** TOOLSTRIP METHODS ********************/

        /*
         *  Method Name: private void toolStripMenuItemLoadImage_Click(object sender, EventArgs e)
         *  Method Parameters: object sender, EventArgs e
         *  Parameter Description: object sender, EventArgs e: handles event args
         *  Purpose: this method is called as soon as the Load Image option is selected on the winform
         *           It will load in the image which the user selects
         *  
         */
        private void toolStripMenuItemLoadImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Load Image";

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    firstLoaded = new Bitmap(ofd.FileName);
                    
                    this.Invalidate();
                }
            }
            isLoaded = true;
        }

        /*
         *  Method Name: private void toolStripMenuEmbedMessage_Click(object sender, EventArgs e)
         *  Method Parameters: object sender, EventArgs e
         *  Parameter Description: object sender, EventArgs e: handles event args
         *  Purpose: this method is called as soon as the embed image option is selected on the winform
         *           It will call the appropriate methods in order to have the image encoded
         *  
         */
        private void toolStripMenuEmbedMessage_Click(object sender, EventArgs e)
        {
            isImageEmbedded = true; 
            isEmbedImage = true;
            isGetMessage = false; 
            this.Invalidate(); 
        }

        /*
         *  Method Name: private void toolStripMenuItemGetMessage_Click(object sender, EventArgs e)
         *  Method Parameters: object sender, EventArgs e
         *  Parameter Description: object sender, EventArgs e: handles event args
         *  Purpose: this method is called as soon as the decode image option is selected on the winform
         *           It will call the appropriate methods in order to have the image decoded and the message 
         *           displayed
         *  
         */
        private void toolStripMenuItemGetMessage_Click(object sender, EventArgs e)
        {
            if (isImageEmbedded == true)
            {
                isEmbedImage = false;
                isGetMessage = true;
                this.Invalidate(); 
            }
        }










        /******************* IMAGE PROCESSING METHODS *****************/


        /*
         *  Method Name: private Bitmap embedMessageInImage(Bitmap bmp)
         *  Method Parameters: Bitmap bmp
         *  Parameter Description: Bitmap bmp: This is the bitmap image passed in, in order to
         *                         encode the message
         *  Purpose: this method is called as soon as the isEmbedImage Boolean is set to true.
         *           This method will take in the original bitmap bytes and return an encoded version
         *           of the bytes to be stored away in a new bitmap.
         *  
         */

        private Bitmap embedMessageInImage(Bitmap bmp)
        {
            arrays = Encoding.ASCII.GetBytes(MESSAGE); 
            byte noMoreBytesInMessage = 0;
            int arrayLength = arrays.Length-1;
            int countOfMessageBytes = 0;

            unsafe
            {
                //create an empty bitmap the same size as original
                Bitmap newBitmap = new Bitmap(bmp.Width, bmp.Height);

                //lock the original bitmap in memory
                System.Drawing.Imaging.BitmapData originalData = bmp.LockBits(
                   new Rectangle(0, 0, bmp.Width, bmp.Height),
                   System.Drawing.Imaging.ImageLockMode.ReadOnly, 
                   System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                //lock the new bitmap in memory
                System.Drawing.Imaging.BitmapData newData = newBitmap.LockBits(
                   new Rectangle(0, 0, bmp.Width, bmp.Height),
                   System.Drawing.Imaging.ImageLockMode.WriteOnly, 
                   System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                //set the number of bytes per pixel
                int pixelSize = 3;

                for (int y = 0; y < bmp.Height; y++){
                    //get the data from the original image
                    byte* originalImageRow = (byte*)originalData.Scan0 + (y * originalData.Stride);

                    //get the data from the new image
                    byte* newImageRow = (byte*)newData.Scan0 + (y * newData.Stride);

                    for (int x = 0; x < bmp.Width; x++){

                        if (countOfMessageBytes <= arrayLength)
                        {
                            byte b = (byte)(originalImageRow[x * pixelSize + 0]); // B
                            byte theBlueByte = changeEachBit(b, BLUE, arrays[countOfMessageBytes]);

                            byte g = (byte)(originalImageRow[x * pixelSize + 1]); // G
                            byte theGreenByte = changeEachBit(g, GREEN, arrays[countOfMessageBytes]);

                            byte r = ((byte)(originalImageRow[x * pixelSize + 2])); //R
                            byte theRedByte = changeEachBit(r, RED, arrays[countOfMessageBytes]);

                            newImageRow[x * pixelSize] = theBlueByte; //B
                            newImageRow[x * pixelSize + 1] = theGreenByte; //G
                            newImageRow[x * pixelSize + 2] = theRedByte; //R

                            countOfMessageBytes++;
                        }
                        else
                        {
                            byte b = (byte)(originalImageRow[x * pixelSize + 0]); // B
                            byte theBlueByte = changeEachBit(b, BLUE, noMoreBytesInMessage);

                            byte g = (byte)(originalImageRow[x * pixelSize + 1]); // G
                            byte theGreenByte = changeEachBit(g, GREEN, noMoreBytesInMessage);

                            byte r = ((byte)(originalImageRow[x * pixelSize + 2])); //R
                            byte theRedByte = changeEachBit(r, RED, noMoreBytesInMessage);

                            newImageRow[x * pixelSize] = theBlueByte; //B
                            newImageRow[x * pixelSize + 1] = theGreenByte; //G
                            newImageRow[x * pixelSize + 2] = theRedByte; //R
                        }

                    }
                }

                //unlock the bitmaps
                newBitmap.UnlockBits(newData);
                bmp.Save("test1.bmp"); 
                bmp.UnlockBits(originalData);
                newBitmap.Save("test2.bmp");
                imageEmbedded = newBitmap;
                return newBitmap;
            }
        }


        /*
        *  Method Name: private Bitmap getEmbeddedMessage(Bitmap bmp)
        *  Method Parameters: Bitmap bmp
        *  Parameter Description: Bitmap bmp: This is the bitmap image passed in, in order to
        *                         decode the message
        *  Purpose: this method is called as soon as the isGetMessage Boolean is set to true.
        *           This method will take in the encoded bitmap bytes and extract the message from the
        *           bytes of the bitmap. 
        *  
        */
        private void getEmbeddedMessage(Bitmap bmp)
        {
                unsafe
                {
                    //create an empty bitmap the same size as original
                    Bitmap newBitmap = new Bitmap(bmp.Width, bmp.Height);

                    //lock the original bitmap in memory
                    System.Drawing.Imaging.BitmapData originalData = bmp.LockBits(
                       new Rectangle(0, 0, bmp.Width, bmp.Height),
                       System.Drawing.Imaging.ImageLockMode.ReadOnly,
                       System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                    //lock the new bitmap in memory
                    System.Drawing.Imaging.BitmapData newData = newBitmap.LockBits(
                       new Rectangle(0, 0, bmp.Width, bmp.Height),
                       System.Drawing.Imaging.ImageLockMode.WriteOnly,
                       System.Drawing.Imaging.PixelFormat.Format24bppRgb);

                    //set the number of bytes per pixel
                    int pixelSize = 3;

                    for (int y = 0; y < bmp.Height; y++)
                    {
                        //get the data from the original image
                        byte* originalImageRow = (byte*)originalData.Scan0 + (y * originalData.Stride);

                        //get the data from the new image
                        byte* newImageRow = (byte*)newData.Scan0 + (y * newData.Stride);

                        for (int x = 0; x < bmp.Width; x++)
                        {

                            byte b = (byte)(originalImageRow[x * pixelSize + 0]); // B
                            getEachBitOfMessage(b, BLUE);

                            byte g = (byte)(originalImageRow[x * pixelSize + 1]); // G
                            getEachBitOfMessage(g, GREEN);

                            byte r = ((byte)(originalImageRow[x * pixelSize + 2])); //R
                            getEachBitOfMessage(r, RED);

                        }
                    }

                    //unlock the bitmaps
                    newBitmap.UnlockBits(newData);
                    bmp.UnlockBits(originalData);
                    string final = string.Join(" ", finalMessage.ToArray());
                    textBox1.AppendText(final);
                }
        }






        /*
         *  Method Name: private byte changeEachBit(byte byteToManipulate, int colour, byte theMessage)
         *  Method Parameters: Bitmap bmp
         *  Parameter Description: byte byteToManipulate: This is the byte of colour we are manipulating, in order to
         *                         encode the message.
         *  Purpose: this method is called as soon as a byte of colour has been extracted from the bitmap.
         *           This method will take in the byte of colour and place bits of the message into the byte of
         *           colour. The byte is then returned and placed into the image. 
         *  
         */

        private byte changeEachBit(byte byteToManipulate, int colour, byte theMessage)
        {

            byte value = 0;
            byte returnByte = 0; 

            // modifying certain bits (depending on the colour) to encode the message
            if (colour == BLUE)
            {
               value= (byte)(theMessage & BValueMask);
               value = (byte)(value>>5); 
               returnByte = (byte)(byteToManipulate & BlueMask);
               returnByte = (byte)(returnByte | value); 
                
            }
            else if (colour == GREEN)
            {
                value = (byte)(theMessage & GValueMask);
                value = (byte)(value >> 3);
                returnByte = (byte)(byteToManipulate & GreenMask);
                returnByte = (byte)(returnByte | value);

            }
            else if (colour == RED)
            {
                value = (byte)(theMessage & RValueMask);
                returnByte = (byte)(byteToManipulate & RedMask);
                returnByte = (byte)(returnByte | value);
            }

            return returnByte;
        }




        /*
         *  Method Name: private void getEachBitOfMessage(byte byteToManipulate, int colour)
         *  Method Parameters: void
         *  Parameter Description: void
         *  Purpose: this method is called as soon as a byte of colour has been extracted from the encoded bitmap.
         *           This method will take in the byte of colour and extract the bits of the message into an
         *           empty byte. It will then place the byte into a List of Strings - the List will contain the
         *           encoded message!
         *  
         */

        private void getEachBitOfMessage(byte byteToManipulate, int colour)
        {
            byte value = 0;

            // retrieving the modified bits (depending on the colour) to read back the encoded message
            if (countToByte == 3)
            {
                byte blueAreaInTotal = 0;
                byte greenAreaInTotal = 0;
                byte redAreaInTotal = 0;
                byte total = 0; 

                redAreaInTotal = (byte)(redCount);
                blueAreaInTotal = (byte)(blueCount << 5);
                greenAreaInTotal = (byte)(greenCount << 3);

                total = (byte)(total | redAreaInTotal); 
                total = (byte)(total | blueAreaInTotal);
                total = (byte)(total | greenAreaInTotal);

                char val = Convert.ToChar(total);
                String nextChar = val.ToString();
                finalMessage.Add(nextChar);
                
                redCount = 0;
                blueCount = 0;
                greenCount = 0; 
                countToByte = 0; 
            }

            if (colour == BLUE)
            {
                value = (byte)(byteToManipulate & InverseBlueMask);
                blueCount = value; 
            }
            else if (colour == GREEN)
            {
                value = (byte)(byteToManipulate & InverseGreenMask);
                greenCount = value; 

            }
            else if (colour == RED)
            {
                value = (byte)(byteToManipulate & InverseRedMask);
                redCount = value; 
            }

            countToByte++; 
        }

    }
}