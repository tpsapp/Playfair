using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Playfair
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnEncrypt_Click(object sender, EventArgs e)
        {
            if ((tbKeyword.Text != "") && (rtbInput.Text != ""))
            {
                string sKey = tbKeyword.Text.ToLower();
                string sGrid = null;
                string sAlpha = "abcdefghiklmnopqrstuvwxyz";
                string sInput = rtbInput.Text.ToLower();
                string sOutput = "";
                Regex rgx = new Regex("[^a-z-]");

                sKey = rgx.Replace(sKey, "");

                sKey = sKey.Replace('j', 'i');

                for (int i = 0; i < sKey.Length; i++)
                {
                    if ((sGrid == null) || (!sGrid.Contains(sKey[i])))
                    {
                        sGrid += sKey[i];
                    }
                }

                for (int i = 0; i < sAlpha.Length; i++)
                {
                    if (!sGrid.Contains(sAlpha[i]))
                    {
                        sGrid += sAlpha[i];
                    }
                }

                sInput = rgx.Replace(sInput, "");

                sInput = sInput.Replace('j', 'i');

                for (int i = 0; i < sInput.Length; i += 2)
                {
                    if (((i + 1) < sInput.Length) && (sInput[i] == sInput[i + 1]))
                    {
                        sInput = sInput.Insert(i + 1, "x");
                    }
                }

                if ((sInput.Length % 2) > 0)
                {
                    sInput += "x";
                }

                int iTemp = 0;
                do
                {
                    int iPosA = sGrid.IndexOf(sInput[iTemp]);
                    int iPosB = sGrid.IndexOf(sInput[iTemp + 1]);
                    int iRowA = iPosA / 5;
                    int iColA = iPosA % 5;
                    int iRowB = iPosB / 5;
                    int iColB = iPosB % 5;

                    if (iColA == iColB)
                    {
                        iPosA += 5;
                        iPosB += 5;
                    }
                    else
                    {
                        if (iRowA == iRowB)
                        {
                            if (iColA == 4)
                            {
                                iPosA -= 4;
                            }
                            else
                            {
                                iPosA += 1;
                            }
                            if (iColB == 4)
                            {
                                iPosB -= 4;
                            }
                            else
                            {
                                iPosB += 1;
                            }
                        }
                        else
                        {
                            if (iRowA < iRowB)
                            {
                                iPosA -= iColA - iColB;
                                iPosB += iColA - iColB;
                            }
                            else
                            {
                                iPosA += iColB - iColA;
                                iPosB -= iColB - iColA;
                            }
                        }
                    }

                    if (iPosA >= sGrid.Length)
                    {
                        iPosA = 0 + (iPosA - sGrid.Length);
                    }

                    if (iPosB >= sGrid.Length)
                    {
                        iPosB = 0 + (iPosB - sGrid.Length);
                    }

                    if (iPosA < 0)
                    {
                        iPosA = sGrid.Length + iPosA;
                    }

                    if (iPosB < 0)
                    {
                        iPosB = sGrid.Length + iPosB;
                    }

                    sOutput += sGrid[iPosA].ToString() + sGrid[iPosB].ToString();

                    iTemp += 2;
                } while (iTemp < sInput.Length);

                rtbOutput.Text = sOutput;
            }
            else
            {
                MessageBox.Show("The Keyword and Input Text fields are required to contain text.");
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if ((tbKeyword.Text != "") && (rtbInput.Text != ""))
            {
                string sKey = tbKeyword.Text.ToLower();
                string sGrid = null;
                string sAlpha = "abcdefghiklmnopqrstuvwxyz";
                string sInput = rtbInput.Text.ToLower();
                string sOutput = "";

                sKey = sKey.Replace('j', 'i');

                for (int i = 0; i < sKey.Length; i++)
                {
                    if ((sGrid == null) || (!sGrid.Contains(sKey[i])))
                    {
                        sGrid += sKey[i];
                    }
                }

                for (int i = 0; i < sAlpha.Length; i++)
                {
                    if (!sGrid.Contains(sAlpha[i]))
                    {
                        sGrid += sAlpha[i];
                    }
                }

                int iTemp = 0;
                do
                {
                    int iPosA = sGrid.IndexOf(sInput[iTemp]);
                    int iPosB = sGrid.IndexOf(sInput[iTemp + 1]);
                    int iRowA = iPosA / 5;
                    int iColA = iPosA % 5;
                    int iRowB = iPosB / 5;
                    int iColB = iPosB % 5;

                    if (iColA == iColB)
                    {
                        iPosA -= 5;
                        iPosB -= 5;
                    }
                    else
                    {
                        if (iRowA == iRowB)
                        {
                            if (iColA == 0)
                            {
                                iPosA += 4;
                            }
                            else
                            {
                                iPosA -= 1;
                            }
                            if (iColB == 0)
                            {
                                iPosB += 4;
                            }
                            else
                            {
                                iPosB -= 1;
                            }
                        }
                        else
                        {
                            if (iRowA < iRowB)
                            {
                                iPosA -= iColA - iColB;
                                iPosB += iColA - iColB;
                            }
                            else
                            {
                                iPosA += iColB - iColA;
                                iPosB -= iColB - iColA;
                            }
                        }
                    }

                    if (iPosA > sGrid.Length)
                    {
                        iPosA = 0 + (iPosA - sGrid.Length);
                    }

                    if (iPosB > sGrid.Length)
                    {
                        iPosB = 0 + (iPosB - sGrid.Length);
                    }

                    if (iPosA < 0)
                    {
                        iPosA = sGrid.Length + iPosA;
                    }

                    if (iPosB < 0)
                    {
                        iPosB = sGrid.Length + iPosB;
                    }

                    sOutput += sGrid[iPosA].ToString() + sGrid[iPosB].ToString();

                    iTemp += 2;
                } while (iTemp < sInput.Length);

                rtbOutput.Text = sOutput;
            }
            else
            {
                MessageBox.Show("The Keyword and Input Text fields are required to contain text.");
            }
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            string sHelp = "How to Encrypt text:\n";
            sHelp += "1. Enter a keyword in the Keyword field.  All non-alphabetical characters will be removed from this field.\n";
            sHelp += "2. Enter the text you would like to encrypt in to the Input Text box.  All non-alphabetical characters will be removed from this field and the letter j will be replaced with the letter i.\n";
            sHelp += "3. Click the Encrypt button.\n";
            sHelp += "4. The encrypted text will appear in the Output Text box.\n";
            sHelp += "\n";
            sHelp += "How to Decrypt text:\n";
            sHelp += "1. Enter the keyword in the Keyword field.  All non-alphabetical characters will be removed from this field.\n";
            sHelp += "2. Enter the text you would like to decrypt in to the Input Text box.  All non-alphabetical characters will be removed from this field.\n";
            sHelp += "3. Click the Decrypt button.\n";
            sHelp += "4. The decrypted text will appear in the Output Text box.\n";
            sHelp += "\n";
            sHelp += "This program was created by Thomas Sapp and is Copyrighted © 2010 by Sappsworld Programming.\n";
            sHelp += "The source code is available at http://code.sappsworld.com\n";
            MessageBox.Show(sHelp, "Playfair Converter Help...", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
