using GameOOP;
using System.Windows.Forms;
using System.Xml.Linq;
using static GameOOP.Engine;
using static GameOOP.Textures;
using static GUI.Form1.DialogueEngine;
using static GUI.Form1.Engine_GUI;
namespace GUI
{
    public partial class FormStart : Form
    {
        private void SavesWindow()
        {
            Form savesForm = new Form
            {
                Text = "Save Files",
                Size = new Size(300, 400)
            };

            ListBox listBoxFiles = new ListBox
            {
                Location = new Point(10, 10),
                Size = new Size(260, 300)
            };

            Button btnLoadSave = new Button
            {
                Text = "Load Save",
                Location = new Point(10, 320),
                Size = new Size(100, 30)
            };

            btnLoadSave.Click += (s, e) =>
            {
                if (listBoxFiles.SelectedItem != null)
                {
                    string selectedSave = listBoxFiles.SelectedItem.ToString();
                    LoadGame(selectedSave);
                    savesForm.Close();
                }
                else
                {
                    MessageBox.Show("Please select a save file to load.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            };

            string savesFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Saves");

            if (!Directory.Exists(savesFolderPath))
            {
                MessageBox.Show("No saves folder found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string[] filePaths = Directory.GetFiles(savesFolderPath);
            if (filePaths.Length == 0)
            {
                MessageBox.Show("No save files found.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (string filePath in filePaths)
            {
                string fileName = Path.GetFileNameWithoutExtension(filePath);
                listBoxFiles.Items.Add(fileName);
            }

            savesForm.Controls.Add(listBoxFiles);
            savesForm.Controls.Add(btnLoadSave);

            savesForm.ShowDialog();
        }
        private void LoadGame(string saveName)
        {
            MessageBox.Show($"Loading game: {saveName}");
            string savesFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Saves");
            string saveFilePath = Path.Combine(savesFolderPath, $"{saveName}.txt");
            using (StreamReader reader = new StreamReader(saveFilePath, true))
            {
                string infa = reader.ReadToEnd().Trim('[', ']');
                string[] array = infa.Split("\n");
                foreach (string unit in array)
                {
                    if (!string.IsNullOrWhiteSpace(unit))
                    {
                        string nameOfValue = unit.Substring(0, unit.IndexOf(','));
                        string value = unit.Substring(unit.LastIndexOf(',') + 1).Trim();
                        switch (nameOfValue)
                        {
                            case "healthPoints":
                            case "numberOfField":
                            case "playerActualX":
                            case "playerActualY":
                            case "playerNextX":
                            case "playerNextY":
                            case "coins":
                                SaveGameStats.SaveCollection[nameOfValue] = Convert.ToInt32(value);
                                break;
                            case "InDialogue":
                                SaveGameStats.SaveCollection[nameOfValue] = Convert.ToBoolean(value);
                                break;
                            case "dirOfMove":
                                SaveGameStats.SaveCollection[nameOfValue] = value;
                                break;
                            default:
                                SaveGameStats.SaveCollection[nameOfValue] = value;
                                break;
                        }

                    }
                }

            }
            Form1 form1 = new Form1();
            form1.Show();
        }
        public FormStart()
        {
            Controls.Add(startButton());
            Controls.Add(LoadGameButton());
            Controls.Add(exitButton());
            Controls.Add(Logo());
            Text = "Lost Soul(Menu)";
            Size = new Size(1920, 1080);
            WindowState = FormWindowState.Maximized;
            //Paint += new PaintEventHandler(Engine_GUI.MainRender);

        }
        public PictureBox Logo()
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = Image.FromFile("E:\\gameProject\\GUI\\ButtonPictures\\logo.png");
            pictureBox.Location = new Point(550, 35);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Size = new Size(800, 600);
            pictureBox.Show();
            return pictureBox;
        }
        public PictureBox startButton()
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = Image.FromFile("E:\\gameProject\\GUI\\ButtonPictures\\startbutton.png");
            pictureBox.Location = new Point(350, 570);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Size = new Size(400, 250);
            pictureBox.Cursor = Cursors.Hand;
            pictureBox.Show();
            pictureBox.Click += StartButton_Click;
            pictureBox.MouseEnter += StartButton_MouseEnter;
            pictureBox.MouseLeave += StartButton_MouseLeave;
            return pictureBox;
        }
        public PictureBox LoadGameButton()
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = Image.FromFile("E:\\gameProject\\GUI\\ButtonPictures\\loadgamepicture.png");
            pictureBox.Location = new Point(750, 570);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Size = new Size(400, 250);
            pictureBox.Cursor = Cursors.Hand;
            pictureBox.Show();
            pictureBox.Click += LoadGameButton_Click;
            return pictureBox;
        }

        
        public PictureBox exitButton()
        {
            PictureBox pictureBox = new PictureBox();
            pictureBox.Image = Image.FromFile("E:\\gameProject\\GUI\\ButtonPictures\\exitbutton.png");
            pictureBox.Location = new Point(1200, 620);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Size = new Size(300, 150);
            pictureBox.Cursor = Cursors.Hand;
            pictureBox.Show();
            pictureBox.Click += ExitButton_Click;
            pictureBox.MouseEnter += ExitButton_MouseEnter;
            pictureBox.MouseLeave += ExitButton_MouseLeave;
            return pictureBox;
        }
        private void StartButton_Click(object sender, EventArgs e)
        {
            Form1 form = new Form1();
            form.Show();
            Hide();
        }
        private void StartButton_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            pictureBox.Image = Image.FromFile("E:\\gameProject\\GUI\\ButtonPictures\\startbutton2.png");
            
        }
        private void StartButton_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            pictureBox.Image = Image.FromFile("E:\\gameProject\\GUI\\ButtonPictures\\startbutton.png");
        }
        private void LoadGameButton_Click(object? sender, EventArgs e)
        {
            SavesWindow();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void ExitButton_MouseEnter(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            pictureBox.Image = Image.FromFile("E:\\gameProject\\GUI\\ButtonPictures\\exitbutton2.png");
        }
        private void ExitButton_MouseLeave(object sender, EventArgs e)
        {
            PictureBox pictureBox = sender as PictureBox;
            pictureBox.Image = Image.FromFile("E:\\gameProject\\GUI\\ButtonPictures\\exitbutton.png");
        }
        
    }
    public partial class FormSaves : Form
    {
        public FormSaves()
        {
            Controls.Add(listBoxFiles);
            Text = "Load Game";
            Size = new Size(1920, 1080);
            WindowState = FormWindowState.Maximized;
        }
        public static ListBox listBoxFiles = new ListBox()
        {
            Location = new Point(450, 250),
            Size = new Size(1000, 700),
        };
        Button btnLoadSave = new Button
        {
            Text = "Load Save",
            Location = new Point(10, 320),
            Size = new Size(100, 30)
        };
    }
    public partial class Form1 : Form
    {
        public static Button buttonM;
        public static Button buttonF;
        public static Panel panelDialogueM;
        public static Panel panelDialogueF;
        public static Panel panelCoin;
        public static Panel panelPlayer;
        public static Panel panelHUD;
        public static Panel panelDeathScreen;
        public static Label labelDeathScreen;
        public static Label labelHealthpoints;
        public static Label labelCoins;
        public static string[,] currentModelOfPlayer = GameOOP.Textures.Player.normalModelOfPlayerR;
        public class DialogueEngine
        {
            public class DialogueAnswer
            {
                public string Text { get; set; }
                public Action<Player> OnSelect { get; set; }

                public DialogueAnswer(string text, Action<Player> onSelect)
                {
                    Text = text;
                    OnSelect = onSelect;
                }
            }

            public class Dialogue
            {
                public string Speaker { get; set; }
                public string Text { get; set; }
                public List<DialogueAnswer> AnswerLine { get; set; }

                public Dialogue(string speaker, string text)
                {
                    Speaker = speaker;
                    Text = text;
                    AnswerLine = new List<DialogueAnswer>();
                }

                public void Answer(string text, Action<Player> onSelect)
                {
                    AnswerLine.Add(new DialogueAnswer(text, onSelect));
                }
            }

            public class Player
            {
                public void DeathScreen()
                {
                    panelDeathScreen.Show();
                    labelDeathScreen.Show();
                }
            }

            public class MerchantDialogue : Dialogue
            {
                public MerchantDialogue() : base("Merchant", "Hello, Stranger!\nHow did you get here?")
                {
                    Answer("Hi! Um... I don't even know.\nWho are you?", (player) =>
                    {
                        Text = "I'm a merchant in this strange town.\nI sell soooo weird and interesting things.\nThey will interest you!";
                        AnswerLine.Clear();
                        Answer("Oh wow, that's so cool\nbut I have no money.", (p) =>
                        {
                            Text = "Ohh, dear, that's not a problem.\nYou can sell me something...\nWhat about your soul?";
                            AnswerLine.Clear();
                            Answer("Wait... What? What do you mean?", (pl) =>
                            {
                                Text = "Just joking heh)))";
                                AnswerLine.Clear();
                                Answer("End dialogue", (pl1) =>
                                {
                                    AnswerLine.Clear();
                                    panelDialogueM.Controls.Clear();
                                    Task.Delay(100).ContinueWith(_ =>
                                    {
                                        panelDialogueM.Invoke((MethodInvoker)(() => panelDialogueM.Visible = false));
                                    });
                                });
                            });
                            Answer("Im ready to sell my soul", (pl) =>
                            {
                                pl.DeathScreen();
                            });
                        });
                        Answer("can you tell me\nabout this town?", (p) =>
                        {
                            Text = "This place once was popular among\nadventurers, merchants, and other people,\nbut everything changed since\n(unknown) became our governor";
                            AnswerLine.Clear();
                            Answer("god, what happened\nto this place?", (pl) =>
                            {
                                Text = "Most of the towns population\ndied or went missing.\nSince then, strange personalities\nbegan to appear";
                                AnswerLine.Clear();
                                Answer("\n\nEnd dialogue", (pl1) =>
                                {
                                    AnswerLine.Clear();
                                    panelDialogueM.Controls.Clear();
                                    Task.Delay(100).ContinueWith(_ =>
                                    {
                                        panelDialogueM.Invoke((MethodInvoker)(() => panelDialogueM.Visible = false));
                                    });
                                });
                            });
                            Answer("who is that (unknown)?\nis he villain?", (pl) =>
                            {
                                Text = "%A$NS#@!H#H$*J(%&K#%J[@L%H$F#&*](?>TR<Y:";
                                AnswerLine.Clear();
                                Answer("\n\nEnd dialogue", (pl1) =>
                                {
                                    pl1.DeathScreen();
                                    AnswerLine.Clear();
                                    panelDialogueM.Controls.Clear();
                                    Task.Delay(100).ContinueWith(_ =>
                                    {
                                        panelDialogueM.Invoke((MethodInvoker)(() => panelDialogueM.Visible = false));
                                    });
                                });
                            });
                        });
                    });
                    Answer("Hello, im just taking a walk", (player) =>
                    {
                        Text = "You choose the wrong\nplace to walk!\nLook around sometimes";
                        AnswerLine.Clear();
                        Answer("End dialogue", (pl1) =>
                        {
                            AnswerLine.Clear();
                            panelDialogueM.Controls.Clear();
                            Task.Delay(100).ContinueWith(_ =>
                            {
                                panelDialogueM.Invoke((MethodInvoker)(() => panelDialogueM.Visible = false));
                            });
                        });
                    });

                }
            }
            public class FrogDialogue : Dialogue
            {
                public FrogDialogue() : base("Froggy", "Hello, dear player!\nI am froggy\nI am here to teach you the basics of this game.")
                {
                    Answer("press me to continue", (player) =>
                    {
                        Text = "to move, use the keys (w), (a), (s), (d).\nto interact with objects, use the button (INTERACT)\nTo leave the game use the key (esc)";
                        AnswerLine.Clear();
                        Answer("press me to continue", (p) =>
                        {
                            Text = "In dialogue you can answer, (if it's possible)\non the right side of dialogue window\nyou can see your answer lines\nlets practice!";
                            AnswerLine.Clear();
                            Answer("press me to continue", (pl) =>
                            {
                                Text = "Hello, how are you?\n\n(click on the line you choose)";
                                AnswerLine.Clear();
                                Answer("Hi, froggy!", (pl1) =>
                                {
                                    Text = "Nice! you did it very well\nso i think you are completely ready\ngood luck to you, stranger";
                                    AnswerLine.Clear();
                                    Answer("press me to continue", (pl2) =>
                                    {
                                        Text = "do not trust anyone here!";
                                        AnswerLine.Clear();
                                        Answer("press me to leave", (pl3) =>
                                        {
                                            AnswerLine.Clear();
                                            panelDialogueM.Controls.Clear();
                                            Task.Delay(100).ContinueWith(_ =>
                                            {
                                                panelDialogueM.Invoke((MethodInvoker)(() => panelDialogueM.Visible = false));
                                            });
                                        });
                                    });
                                });
                            });
                        });
                    });
                }
            }

            public class DialogueEngineVisual
            {
                private Panel panelDialogue;

                public DialogueEngineVisual(Panel panelDialogue)
                {
                    this.panelDialogue = panelDialogue;
                }

                public void StartDialogue(Dialogue dialogue)
                {
                    panelDialogue.Controls.Clear();
                    panelDialogue.Show();
                    panelDialogue.Visible = true;
                    Label labelName = new Label()
                    {
                        Location = new Point(0, panelDialogue.Height / 3),
                        BackColor = Color.LightBlue,
                        AutoSize = true,
                        ForeColor = Color.Black,
                        Font = new System.Drawing.Font("Chiller", 45, FontStyle.Bold),
                        Text = dialogue.Speaker
                    };
                    panelDialogue.Controls.Add(labelName);

                    Label label1 = new Label()
                    {
                        Location = new Point(250, 10),
                        AutoSize = true,
                        ForeColor = Color.Black,
                        Font = new System.Drawing.Font("Chiller", 35, FontStyle.Bold),
                        Text = dialogue.Text
                    };
                    panelDialogue.Controls.Add(label1);

                    int y = 30;
                    foreach (var option in dialogue.AnswerLine)
                    {
                        Label labelAnswer = new Label()
                        {
                            Location = new Point(1300, y),
                            AutoSize = true,
                            ForeColor = Color.Green,
                            Font = new System.Drawing.Font("Castellar", 15, FontStyle.Bold),
                            Text = option.Text
                        };
                        labelAnswer.MouseClick += (sender, e) =>
                        {
                            option.OnSelect(new Player());
                            StartDialogue(dialogue);
                        };
                        panelDialogue.Controls.Add(labelAnswer);
                        y += 90;
                    }
                }
            }

        }
        public class Merchant : DialogueEngine.DialogueEngineVisual
        {
            public Merchant(Panel panelDialogueM) : base(panelDialogueM) 
            {
            }

            public void StartDialogue()
            {
                base.StartDialogue(new MerchantDialogue());
            }
        }
        public class Frog : DialogueEngine.DialogueEngineVisual
        {
            public Frog(Panel panelDialogueM) : base(panelDialogueM)
            {
            }
            public void StartDialogue()
            {
                base.StartDialogue(new FrogDialogue());
            }
        }

        public class Engine_GUI
        {
            public class Textures
            {
                public Brush color { get; set; }
                public string[,] texture { get; set; }
                public Textures(string[,] texture, Brush color)
                {
                    this.color = color;
                    this.texture = texture;
                }
                public static Dictionary<string, Textures> TexturesCollectionGUI = new Dictionary<string, Textures>
                {
                    {"#", new Textures(Walls.wall1, Brushes.Blue) },
                    {"@", new Textures(Walls.wall2, Brushes.Blue) },
                    {" ", new Textures(Clear.clear, Brushes.Black) },
                    {"m", new Textures(NPC.Mercant.movingModelOfMerchantR1, Brushes.Magenta) },
                    {"t11", new Textures(Trees.tree11, Brushes.Green) },
                    {"t12", new Textures(Trees.tree12, Brushes.Green) },
                    {"t21", new Textures(Trees.tree21, Brushes.Green) },
                    {"t22", new Textures(Trees.tree22, Brushes.Green) },
                    {"s", new Textures(Stone.stone1, Brushes.Gray) },
                    {"c", new Textures(Coins.coin1, Brushes.Yellow) },
                    {"f", new Textures(NPC.Frog.frogModel, Brushes.Green) },
                };
            }
            
            public class Render_GUI
            {
                public static void MainRender(object sender, PaintEventArgs e)
                {
                    RenderFULLonGUI(e.Graphics, Render.Field2);
                }
                
                public static void RenderOnGUI(Graphics graphics, string[,] texture, int X, int Y, int sizeX, int sizeY, Brush brush)
                {
                    for (int y = 0; y < texture.GetLength(0); y++)
                    {
                        for (int x = 0; x < texture.GetLength(1); x++)
                        {
                            
                            if (texture[y, x] != " ")
                            {
                                graphics.FillRectangle(brush, x * sizeX + X * sizeX * 13, y * sizeY + Y * sizeY * 9, sizeX, sizeY);
                            }

                        }
                    }
                }
                
                public static void RenderFULLonGUI(Graphics graphics, string[,] field)
                {
                    Render render = new Render();
                    for (int y = 0; y < field.GetLength(0); y++)
                    {
                        for (int x = 0; x < field.GetLength(1); x++)
                        {
                            if(field[y, x] != " ")
                            {
                                RenderOnGUI(graphics, Textures.TexturesCollectionGUI[field[y, x]].texture, x, y, 3, 6, Textures.TexturesCollectionGUI[field[y, x]].color);
                            }
                        }
                    }
                }
            }
            
        }
        public void PropertiesActivation1(Properties_GUI properties, KeyEventArgs keyEventArgs)
        {
            properties.PropertiesActivate(keyEventArgs);
        }
        public abstract class Properties_GUI
        {
            Info info = new Info();
            public abstract void PropertiesActivate(KeyEventArgs keyEventArgs);
            public object name { get; set; }
            public int layerLvl { get; set; }
            public bool IsMovable { get; set; }
            public bool IsResponse { get; set; }
            public bool IsPassable { get; set; }
            public static Dictionary<string, Properties_GUI> PropertiesCollection = new Dictionary<string, Properties_GUI>
                    {
                        {"#", new Wall(false, false, "wallinf")},
                        {"@", new Wall(false, true, "wallinf2")},
                        {" ", new Clear(true, false, "clearinf")},
                        {"m", new Merchant(false, true, "merchantinf")},
                        {"s", new Stone(false, true, "stoneinf")},
                        {"t11", new Tree(false, true, "treeinf")},
                        {"t12", new Tree(false, true, "treeinf")},
                        {"t21", new Tree(false, true, "treeinf")},
                        {"t22", new Tree(false, true, "treeinf")},
                        {"c", new Coin1(false, true, "coin1inf")},
                        {"f", new Frog(false, true, "froginf")},
                    };
            public static Properties_GUI[,] matrix1;
            public static Properties_GUI[,] matrix2;
            static Player playerinf = new Player(true, false, "playerinf");
            static Clear clearinf = new Clear(true, false, "clearinf");
            public static Properties_GUI[,] BuildMatrix(string[,] field)
            {
                Properties_GUI[,] matrix = new Properties_GUI[field.GetLength(0) * 2, field.GetLength(1) * 2];
                for (int i = 0; i < field.GetLength(0); i++)
                {
                    for (int j = 0; j < field.GetLength(1); j++)
                    {
                        if ((field[i, j] == "#" || field[i, j] == "@") && j >= 1 && i >= 1)
                        {
                            matrix[i - 1, j] = PropertiesCollection[field[i, j]];
                            matrix[i, j - 1] = PropertiesCollection[field[i, j]];
                            matrix[i - 1, j - 1] = PropertiesCollection[field[i, j]];

                        }
                        else if (field[i, j] == " " || field[i, j] == "#" || field[i, j] == "@")
                        {
                            matrix[i, j] = PropertiesCollection[field[i, j]];
                        }
                        else
                        {
                            matrix[i, j] = PropertiesCollection[field[i, j]];
                            matrix[i - 1, j] = PropertiesCollection[field[i, j]];
                            matrix[i, j - 1] = PropertiesCollection[field[i, j]];
                            matrix[i - 1, j - 1] = PropertiesCollection[field[i, j]];
                        }

                    }
                }
                return matrix;
            }
            public void HealHp(int hp)
            {
                SaveGameStats.SaveCollection["healthPoints"] = (int)SaveGameStats.SaveCollection["healthPoints"] + hp;
                if ((int)SaveGameStats.SaveCollection["healthPoints"] > 100)
                {
                    SaveGameStats.SaveCollection["healthPoints"] = 100;
                }
                labelHealthpoints.Text = $"HP:{SaveGameStats.SaveCollection["healthPoints"]}";

            }
            public void Damage(int hp)
            {
                SaveGameStats.SaveCollection["healthPoints"] = (int)SaveGameStats.SaveCollection["healthPoints"] - hp;
                if ((int)SaveGameStats.SaveCollection["healthPoints"] < 0)
                {
                    SaveGameStats.SaveCollection["healthPoints"] = 0;
                }
                labelHealthpoints.Text = $"HP:{SaveGameStats.SaveCollection["healthPoints"]}";
            }
            public void coinsVisual(int coins)
            {
                SaveGameStats.SaveCollection["coins"] = (int)SaveGameStats.SaveCollection["coins"] + coins;
                labelCoins.Text = $"COINS:{SaveGameStats.SaveCollection["coins"]}";
                
            }
            public Properties_GUI(bool IsPassable, bool IsResponse)
            {
                this.IsPassable = IsPassable;
                this.IsResponse = IsResponse;
            }
            
            public class Player : Properties_GUI
            {
                public Player(bool IsPassable, bool IsResponse, object name) : base(IsPassable, IsResponse)
                {
                    this.name = name;
                }
                public override void PropertiesActivate(KeyEventArgs keyEventArgs)
                {

                }
            }
            public class Merchant : Properties_GUI
            {
                public Merchant(bool IsPassable, bool IsResponse, object name) : base(IsPassable, IsResponse)
                {
                    this.name = name;
                }

                public override void PropertiesActivate(KeyEventArgs keyEventArgs)
                {
                    buttonM.Show();
                    /*Player1 player = new Player1((int)SaveGameStats.SaveCollection["healthPoints"]);
                    buttonM.MouseClick += (sender, e) =>
                    {
                        buttonM.Hide();
                        Form1.Merchant merchant = new Form1.Merchant(panelDialogueM);
                        merchant.StartDialogue();
                    }; */
                }
            }
            public class Coin1 : Properties_GUI
            {
                public Coin1(bool IsPassable, bool IsResponse, object name) : base(IsPassable, IsResponse)
                {
                    this.name = name;
                }
                public override void PropertiesActivate(KeyEventArgs keyEventArgs)
                {
                    coinsVisual(1);
                    for (int i = 0; i < Render.Field2.GetLength(0); i++)
                    {
                        for (int j = 0; j < Render.Field2.GetLength(1); j++)
                        {
                            if (Render.Field2[i, j] == "c")
                            {
                                Render.Field2[i, j] = " ";
                            }
                        }
                    }
                    int xx = GetIndexesOfArray2D(matrix2, PropertiesCollection.GetValueOrDefault("c"))[0];
                    int yy = GetIndexesOfArray2D(matrix2, PropertiesCollection.GetValueOrDefault("c"))[1];
                    matrix2[yy, xx] = PropertiesCollection.GetValueOrDefault(" ");
                    matrix2[yy + 1, xx] = PropertiesCollection.GetValueOrDefault(" ");
                    matrix2[yy, xx + 1] = PropertiesCollection.GetValueOrDefault(" ");
                    matrix2[yy + 1, xx + 1] = PropertiesCollection.GetValueOrDefault(" ");
                }
            }
            public class Wall : Properties_GUI
            {
                public Wall(bool IsPassable, bool IsResponse, object name) : base(IsPassable, IsResponse)
                {
                    this.name = name;
                }
                public override void PropertiesActivate(KeyEventArgs keyEventArgs)
                {

                    /*if (name == "wallinf2")
                    {
                        Render render = new Render();
                        int width = render.FieldCollection[SaveGameStats.numberOfField].GetLength(1) * 13 * 3;
                        if (SaveGameStats.playerActualX > width / 2)
                        {
                            SaveGameStats.playerActualX = width - SaveGameStats.playerActualX + 6;
                            SaveGameStats.numberOfField += 1;
                            SaveGameStats.SaveCollection["numberOfField"] = SaveGameStats.numberOfField;
                        }
                        else
                        {
                            SaveGameStats.playerActualX = width - SaveGameStats.playerActualX - 14 * 3;
                            SaveGameStats.numberOfField -= 1;
                            SaveGameStats.SaveCollection["numberOfField"] = SaveGameStats.numberOfField;
                        }
                    }*/
                }
            }
            public class Stone : Properties_GUI
            {
                public Stone(bool IsPassable, bool IsResponse, object name) : base(IsPassable, IsResponse)
                {
                    this.name = name;
                }

                public override void PropertiesActivate(KeyEventArgs keyEventArgs)
                {

                }
            }
            public class Clear : Properties_GUI
            {
                public Clear(bool IsPassable, bool IsResponse, object name) : base(IsPassable, IsResponse)
                {
                    this.name = name;
                }

                public override void PropertiesActivate(KeyEventArgs keyEventArgs)
                {

                }
            }
            public class Tree : Properties_GUI
            {
                public Tree(bool IsPassable, bool IsResponse, object name) : base(IsPassable, IsResponse)
                {
                    this.name = name;
                }

                public override void PropertiesActivate(KeyEventArgs keyEventArgs)
                {

                }
            }
            public class Frog : Properties_GUI
            {
                public Frog(bool IsPassable, bool IsResponse, object name) : base(IsPassable, IsResponse)
                {
                    this.name = name;
                }

                public override void PropertiesActivate(KeyEventArgs keyEventArgs)
                {
                    buttonF.Show();
                }
            }
            public static void SetMatrix(int x, int y)
            {
                matrix2[y, x] = new Player(true, false, "playerinf");
            }
            public static void ResetMatrix(int x, int y)
            {
                matrix2[y, x] = new Clear(true, false, "clearinf");
            }
            public static int[] GetIndexesOfArray2D(Properties_GUI[,] array, Properties_GUI unit)
            {
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    for (int j = 0; j < array.GetLength(1); j++)
                    {
                        if (array[i, j] == unit)
                        {
                            return [j, i];
                        }
                    }
                }
                return [-1, -1];
            }
        }
   
        public static void RenderPlayer(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Render_GUI.RenderOnGUI(g, currentModelOfPlayer, 0, 0, 3, 6, Brushes.Green);

        }
        public void Save(string name)
        {
            string savesFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Saves");
            string saveFilePath = Path.Combine(savesFolderPath, $"{name}.txt");
            using (StreamWriter writer = new StreamWriter(saveFilePath, false))
            {
                foreach (object unit in SaveGameStats.SaveCollection)
                {
                    string text = Convert.ToString(unit).Trim('[', ']');
                    writer.WriteLine(text);
                }
            }
        }
        public void MovingGUI(object sender, KeyEventArgs e)
        {
            Info ObjectForPropertiesAct = new Info();
            Render render = new Render();
            SaveGameStats.playerNextX = SaveGameStats.playerActualX;
            SaveGameStats.playerNextY = SaveGameStats.playerActualY;
            Properties_GUI.SetMatrix(SaveGameStats.playerActualX / (13 * 3), SaveGameStats.playerActualY / (9 * 6));
            switch (e.KeyCode)
            {
                case Keys.W:
                    SaveGameStats.playerNextY -= 10;
                    break;
                case Keys.S:
                    SaveGameStats.playerNextY += 10;
                    break;
                case Keys.A:
                    if(currentModelOfPlayer != GameOOP.Textures.Player.normalModelOfPlayerL)
                    {
                        currentModelOfPlayer = GameOOP.Textures.Player.normalModelOfPlayerL;
                        panelPlayer.Invalidate();
                    }
                    SaveGameStats.playerNextX -= 10;
                    break;
                case Keys.D:
                    if (currentModelOfPlayer != GameOOP.Textures.Player.normalModelOfPlayerR)
                    {
                        currentModelOfPlayer = GameOOP.Textures.Player.normalModelOfPlayerR;
                        panelPlayer.Invalidate();
                    }
                    SaveGameStats.playerNextX += 10;
                    break;
                case Keys.E:
                    Save($"save{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")}");
                    break;
                case Keys.Escape:
                    Environment.Exit(0);
                    break;
            }
            if (Properties_GUI.matrix2[SaveGameStats.playerNextY / (9 * 6), SaveGameStats.playerNextX / (13 * 3)].IsPassable)
            {
                Properties_GUI.ResetMatrix((int)(SaveGameStats.playerActualX / (13 * 3)), (int)(SaveGameStats.playerActualY / (9 * 6)));
                SaveGameStats.playerActualX = SaveGameStats.playerNextX;
                SaveGameStats.playerActualY = SaveGameStats.playerNextY;
                SaveGameStats.SaveCollection["playerActualX"] = SaveGameStats.playerNextX;
                SaveGameStats.SaveCollection["playerActualY"] = SaveGameStats.playerNextY;
                Properties_GUI.SetMatrix((int)(SaveGameStats.playerActualX / (13 * 3)), (int)(SaveGameStats.playerActualY / (9 * 6)));
            }
            else
            {
                if(Properties_GUI.matrix2[SaveGameStats.playerNextY / (9 * 6), SaveGameStats.playerNextX / (13 * 3)] == Properties_GUI.PropertiesCollection["c"])
                {
                    PropertiesActivation1(Properties_GUI.matrix2[SaveGameStats.playerNextY / (9 * 6), SaveGameStats.playerNextX / (13 * 3)], e);
                    this.Invalidate();
                }
                else if(Properties_GUI.matrix2[SaveGameStats.playerNextY / (9 * 6), SaveGameStats.playerNextX / (13 * 3)] == Properties_GUI.PropertiesCollection["m"] || Properties_GUI.matrix2[SaveGameStats.playerNextY / (9 * 6), SaveGameStats.playerNextX / (13 * 3)] == Properties_GUI.PropertiesCollection["f"])
                {
                    PropertiesActivation1(Properties_GUI.matrix2[SaveGameStats.playerNextY / (9 * 6), SaveGameStats.playerNextX / (13 * 3)], e);
                }
                
                
            }
            panelPlayer.Location = new Point(SaveGameStats.playerActualX, SaveGameStats.playerActualY);
        }
        System.Windows.Forms.Label label;
        System.Windows.Forms.Timer timer;
        public string fullText;
        public int currentIndex;
        public void RunField()
        {
            Properties_GUI.matrix2 = Properties_GUI.BuildMatrix(Render.Field2);
            Paint += new PaintEventHandler(Engine_GUI.Render_GUI.MainRender);
        }
        
        public Form1()
        {
            Render.Field2[4, 6] = "f";
            labelHealthpoints = new System.Windows.Forms.Label()
            {
                Location = new Point(40, 40),
                ForeColor = Color.Red,
                Size = new Size(300, 80),
                Font = new System.Drawing.Font("Chiller", 50, FontStyle.Bold),
                Text = $"HP:{SaveGameStats.SaveCollection["healthPoints"]}"
            };
            labelCoins = new System.Windows.Forms.Label()
            {
                Location = new Point(40, 140),
                ForeColor = Color.FromArgb(255, 171, 0),
                Size = new Size(300, 80),
                Font = new System.Drawing.Font("Chiller", 50, FontStyle.Bold),
                Text = $"COINS:{SaveGameStats.SaveCollection["coins"]}"
            };
            panelDialogueM = new Panel()
            {
                Location = new Point(0, 650),
                BackColor = Color.LightGray,
                Size = new Size(1800, 280),
            };
            panelDialogueF = new Panel()
            {
                Location = new Point(0, 650),
                BackColor = Color.LightGray,
                Size = new Size(1800, 280),
            };
            //panelDialogueM.Hide();
            panelDialogueF.Hide();
            panelPlayer = new Panel()
            {
                Location = new Point(SaveGameStats.playerActualX, SaveGameStats.playerActualY),
                Size = new Size(3 * 13, 6 * 9),
            };
            panelPlayer.Paint += new PaintEventHandler(RenderPlayer);
            panelHUD = new Panel()
            {
                BackColor = Color.GreenYellow,
                Location = new Point(1300, 0),
                Size = new Size(500, 600),
                
            };
            buttonM = new Button()
            {
                Location = new Point(1250, 600),
                BackColor = Color.Red,
                ForeColor = Color.LightGray,
                AutoSize = true,
                Font = new System.Drawing.Font("Castellar", 15, FontStyle.Bold),
                Text = "INTERACT",
            };
            buttonF = new Button()
            {
                Location = new Point(1250, 600),
                BackColor = Color.Red,
                ForeColor = Color.LightGray,
                AutoSize = true,
                Font = new System.Drawing.Font("Castellar", 15, FontStyle.Bold),
                Text = "INTERACT",
            };
            panelDeathScreen = new Panel()
            {
                Top = 1,
                Location = new Point(0, 0),
                Size = new Size(1920, 1080),
                BackColor = Color.Black,
            };
            labelDeathScreen = new Label()
            {
                Top = 2,
                Location = new Point(720, 300),
                ForeColor = Color.Red,
                AutoSize = true,
                Font = new System.Drawing.Font("Chiller", 50, FontStyle.Bold),
                Text = "YOU ARE DEAD!"
            };
            InitializeComponent();
            RunField();
            Controls.Add(buttonM);
            Controls.Add(buttonF);
            buttonF.Hide();
            buttonM.Hide();
            Controls.Add(panelDeathScreen);
            panelDeathScreen.Controls.Add(labelDeathScreen);
            labelDeathScreen.Hide();
            panelDeathScreen.Hide();
            this.Controls.Add(panelDialogueM);
            Controls.Add(panelDialogueF);
            Controls.Add(panelPlayer);
            Controls.Add(panelHUD);
            panelHUD.Controls.Add(labelHealthpoints);
            panelHUD.Controls.Add(labelCoins);
            KeyDown += new KeyEventHandler(MovingGUI);
            buttonM.Click += buttonM_Click;
            buttonF.Click += buttonF_Click;
            Text = "Lost Soul";
            Size = new Size(1920, 1080);
            WindowState = FormWindowState.Maximized;
            KeyPreview = true;
            
        }
        private void buttonM_Click(object sender, EventArgs e)
        {
            buttonM.Hide();
            Form1.Merchant merchant = new Form1.Merchant(panelDialogueM);
            merchant.StartDialogue();
        }
        private void buttonF_Click(object sender, EventArgs e)
        {
            buttonF.Hide();
            Form1.Frog frog = new Form1.Frog(panelDialogueM);
            frog.StartDialogue();
        }

    }
}
