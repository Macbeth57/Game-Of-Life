namespace Le_Jeu_De_La_Vie
{
    public partial class Form1 : Form
    {
        int gridWidth = 40;
        int gridHeight = 40;

        int cellHeight = 25;
        int cellWidth = 25;
        int spacing = 0;
        int startX = 100;
        int startY = 100;

        bool isRunning = true; 

        List<List<int>> gridValues = new List<List<int>>();
        Panel[,] gridPanels;
        Button startButton = new Button();


        public Form1()
        {

            InitializeComponent();
            GridInitialization(gridWidth, gridHeight);
            GridGenerator();
            SetInterface();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void GridInitialization(int width, int height)
        {

            for (int i = 0; i < width; i++)
            {

                List<int> innerList = new List<int>();

                for (int j = 0; j < height; j++)
                {
                    innerList.Add(0);
                }

                gridValues.Add(innerList);

            }
        }

        private void GridGenerator()
        {
            gridPanels = new Panel[gridWidth, gridHeight];

            for (int i = 0; i < gridValues.Count; i++)
            {
                for (int j = 0; j < gridValues[i].Count; j++)
                {
                    Panel panel = new Panel();

                    panel.Size = new Size(cellWidth, cellHeight);
                    panel.Location = new Point(startX + cellWidth * j + spacing * j, startY + cellHeight * i + spacing * i);
                    panel.BorderStyle = BorderStyle.Fixed3D;
                    panel.Click += Panel_Click;
                    panel.BackColor = gridValues[i][j] == 0 ? Color.White : Color.Black;

                    this.Controls.Add(panel);
                    gridPanels[i, j] = panel;

                }
            }
        }

        private void GridRefresh()
        {
            
        }

        private void SetInterface()
        {
            //START BUTTON
            Controls.Add(startButton);

            startButton.Size = new Size(120, 50);
            startButton.BackColor = Color.Black;
            startButton.Location = new Point(550, 20);
            startButton.Text = "START";
            startButton.ForeColor = Color.White;
            startButton.Font = new Font(startButton.Font, FontStyle.Bold);
            startButton.Click += StartButton_Click;
        }

        private void Panel_Click(object sender, EventArgs e)
        {
            Panel clickedPanel = sender as Panel;

            int row = (clickedPanel.Location.Y - startY) / (cellHeight + spacing);
            int column = (clickedPanel.Location.X - startX) / (cellWidth + spacing);

            if (clickedPanel != null)
            {
                clickedPanel.BackColor = Color.Black;
                gridValues[row][column] = 1;
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            GameOfLife();
        }

        private async void GameOfLife()
        {

            int totalValueNeighboors;
            

            while (isRunning)
            {

                List<List<int>> copyGridValues = new List<List<int>>(gridValues);

                for (int i = 1; i < gridValues.Count-1; i++)
                {
                    for (int j = 1; j < gridValues[i].Count-1; j++)
                    {

                        totalValueNeighboors = copyGridValues[i + 1][j] + copyGridValues[i][j + 1] + copyGridValues[i + 1][j + 1] + copyGridValues[i - 1][j] + copyGridValues[i][j - 1] + copyGridValues[i - 1][j - 1] + copyGridValues[i + 1][j - 1] + copyGridValues[i - 1][j + 1];

                        if (copyGridValues[i][j] == 1 && (totalValueNeighboors < 2 || totalValueNeighboors > 3)) 
                        {
                            gridValues[i][j] = 0;
                            gridPanels[i, j].BackColor = Color.White;
                        }
                        else if(copyGridValues[i][j] == 0 && totalValueNeighboors == 3)
                        {
                            gridValues[i][j] = 1;
                            gridPanels[i, j].BackColor = Color.Black;
                        }
                       

                        totalValueNeighboors = 0;

                    }
                }

                await Task.Delay(500);

            }
        }

    }
}