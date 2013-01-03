using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Threading;
using System.Windows.Interactivity;
using System.IO.IsolatedStorage;
using System.IO;

namespace GamePack
{
	// Object that keeps information about the Game status
	public class CollisionTag
	{
		private string collisionType;
		private bool isCollision;
		private Vector normal;
		private bool isChangeMotion;
		private int scoreValue;
		private int livesValue;
		private bool restart;
		private bool isGameOver;
		private bool isLevelObject;
		private bool isNextLevel;
		private bool isMoving;
		private double collisionX;
		private double collisionCenter;

		public string CollisionType
		{
			get { return this.collisionType; }
			set { this.collisionType = value; }
		}

		public bool IsCollision
		{
			get { return this.isCollision; }
			set { this.isCollision = value; }
		}

		public Vector Normal
		{
			get { return this.normal; }
			set { this.normal = value; }
		}

		public bool IsChangeMotion
		{
			get { return this.isChangeMotion; }
			set { this.isChangeMotion = value; }
		}

		public int ScoreValue
		{
			get { return this.scoreValue; }
			set { this.scoreValue = value; }
		}
		public int LivesValue
		{
			get { return this.livesValue; }
			set { this.livesValue = value; }
		}
		public bool Restart
		{
			get { return this.restart; }
			set { this.restart = value; }
		}
		public bool IsGameOver
		{
			get { return this.isGameOver; }
			set { this.isGameOver = value; }
		}
		public bool IsLevelObject
		{
			get { return this.isLevelObject; }
			set { this.isLevelObject = value; }
		}
		public bool IsNextLevel
		{
			get { return this.isNextLevel; }
			set { this.isNextLevel = value; }
		}
		public bool IsMoving
		{
			get { return this.isMoving; }
			set { this.isMoving = value; }
		}
		public double CollisionX
		{
			get { return this.collisionX; }
			set { this.collisionX = value; }
		}
		public double CollisionCenter
		{
			get { return this.collisionCenter; }
			set { this.collisionCenter = value; }
		}
	}


    public class GameEnvironment : TriggerAction<DependencyObject>
    {
		// Private Properties
		private Canvas game;
		private Control mainControl;
		private int lives;
		private int lastScore;
		private int Level;

		private DispatcherTimer clock;

		private const int MAX_NUM_LEVELS = 3;

		// List of objects that must be tested against all other objects
		// This improves performance, since we define a limited number of objects
		private List<FrameworkElement> collideWithAllList = new List<FrameworkElement>(30);
		private List<FrameworkElement> collisionList = new List<FrameworkElement>(30);
		private List<FrameworkElement> scoreList = new List<FrameworkElement>();
		private List<FrameworkElement> livesList = new List<FrameworkElement>();
		private List<FrameworkElement> highscoreList = new List<FrameworkElement>();
		private List<FrameworkElement> lastscoreList = new List<FrameworkElement>();
		private List<FrameworkElement> levelObjectsList = new List<FrameworkElement>();


		#region Dependency Properties

		public static readonly DependencyProperty GameOverSoundProperty = DependencyProperty.Register("GameOverSound", typeof(System.Uri), typeof(GameEnvironment), null);
		public static readonly DependencyProperty NextLevelSoundProperty = DependencyProperty.Register("NextLevelSound", typeof(System.Uri), typeof(GameEnvironment), null);
		public static readonly DependencyProperty GameOverStateProperty = DependencyProperty.Register("GameOverState", typeof(string), typeof(GameEnvironment), null);

		[Category("Game States Properties")]
		public System.Uri GameOverSound { get { return (System.Uri)GetValue(GameOverSoundProperty); } set { SetValue(GameOverSoundProperty, value); } }
		[Category("Game States Properties")]
		public System.Uri NextLevelSound { get { return (System.Uri)GetValue(NextLevelSoundProperty); } set { SetValue(NextLevelSoundProperty, value); } }
		[Category("Game States Properties")]
		public string GameOverState { get { return (string)GetValue(GameOverStateProperty); } set { SetValue(GameOverStateProperty, value); } }

        #endregion


		protected override void OnAttached()
		{
			base.OnAttached();
			game = this.AssociatedObject as Canvas;
		}

        protected override void Invoke(object o)
        {

			// Finds the topmost User Control. THis reference is used to trigger User Control States
			DependencyObject parent = this.AssociatedObject as DependencyObject;
			while (parent != null)
			{
				mainControl = parent as Control;
				parent = VisualTreeHelper.GetParent(parent) as DependencyObject;
			}

			// Updates List of elements (Coliisions, Score, Lives, etc...)
			UpdateLists();

			// Keeps the Initial value of Lives
			foreach (TextBlock t in livesList)
			{
				lives = Int16.Parse(t.Text);
			}

			// Starts with Level 1
			Level = 1;
			// Set state to "Level 1"
			if (mainControl != null) VisualStateManager.GoToState(mainControl, "Level1", true);

			// Reads High-score from local file storage
			string highscore = ReadHighscore();

			// Updates High-score text
			foreach (TextBlock h in highscoreList)
			{
				h.Text = highscore;
			}

			// Starts main Game Loop
			CompositionTarget.Rendering += new EventHandler(CollisionCheck_Rendering);

        }

		// Updates Lists of elements that interact with others
		void UpdateLists()
		{
			foreach (FrameworkElement el in game.Children)
			{
				CollisionTag tag = el.Tag as CollisionTag;
				if (tag != null)
				{
					if ((tag.CollisionType == "Collision") && (el.Visibility != Visibility.Collapsed))
					{
						collisionList.Add(el);
						if (tag.IsLevelObject)
						{
							levelObjectsList.Add(el);
						}
					}
					else if (tag.CollisionType == "CollideWithAll")
					{
						collideWithAllList.Add(el);
					}
					else if (tag.CollisionType == "TextScore")
					{
						scoreList.Add(el);
					}
					else if (tag.CollisionType == "TextLives")
					{
						livesList.Add(el);
					}
					else if (tag.CollisionType == "Highscore")
					{
						highscoreList.Add(el);
					}
					else if (tag.CollisionType == "Lastscore")
					{
						lastscoreList.Add(el);
					}
				}
			}
		}

		// Updates only elements necessary for a new Level
		void UpdateListsNewLevel()
		{
			foreach (FrameworkElement el in game.Children)
			{
				CollisionTag tag = el.Tag as CollisionTag;
				if (tag != null)
				{
					if ((tag.CollisionType == "Collision") && (el.Visibility != Visibility.Collapsed))
					{
						collisionList.Add(el);
						if (tag.IsLevelObject)
						{
							levelObjectsList.Add(el);
						}
					}

				}
			}
		}

		void PlaySound(Uri file)
		{
			try
			{
				MediaElement audio = new MediaElement();
				audio.Source = file;
				audio.MediaEnded += delegate
				{
					game.Children.Remove(audio);
				};
				game.Children.Add(audio);
				audio.Play();
			}
			catch
			{
			}
		}


		private Rect GetBoundingBox(FrameworkElement element)
		{
			return new Rect(Canvas.GetLeft(element), Canvas.GetTop(element), element.ActualWidth, element.ActualHeight);
		}


		void CollisionCheck_Rendering(object sender, EventArgs e)
		{
				Rect r1, r2;
				for (int i = 0; i < collideWithAllList.Count; i++)
				{
					for (int j = 0; j < collisionList.Count; j++)
					{
						CollisionTag tag = collisionList[j].Tag as CollisionTag;
						if ((collideWithAllList[i].Visibility == Visibility.Visible) && (collisionList[j].Visibility == Visibility.Visible) && (tag.CollisionType != "Hidden"))
						{
							r1 = GetBoundingBox(collideWithAllList[i]);
							r2 = GetBoundingBox(collisionList[j]);
							r1.Intersect(r2);

							if (r1 != Rect.Empty)
							{
								Vector Normal = new Vector(1, 1);

								// Check which side collided
								if (r1.Width >= r1.Height)
								{
									Normal = new Vector(1, -1);
								}
								else
								{
									Normal = new Vector(-1, 1);
								}

								CollisionTag tag1 = collideWithAllList[i].Tag as CollisionTag;
								CollisionTag tag2 = collisionList[j].Tag as CollisionTag;
								tag1.IsCollision = true;
								tag1.Normal = Normal;
								tag2.IsCollision = true;
								tag2.Normal = Normal;

								// Collision with a Moving Paddle? If so, value = true
								if (tag2.IsMoving)
								{
									tag1.IsMoving = true;
									tag1.CollisionCenter = collisionList[j].Width / 2;
									tag1.CollisionX = Canvas.GetLeft(collisionList[j]);
								}

								// Checks if collision with a Block, then reduce numbers of Blocks left
								if (tag2.IsLevelObject)
								{
									levelObjectsList.Remove(collisionList[j]);
									if (levelObjectsList.Count == 0)
									{
										// If list of objects is Zero, then move to Next Level
										++Level;
										if (Level > MAX_NUM_LEVELS) Level = 1;
										VisualStateManager.GoToState(mainControl, "Reset", true);
										VisualStateManager.GoToState(mainControl, "Level" + Level, true);
										VisualStateManager.GoToState(mainControl, "WaitStart", true);

										tag1.Restart = true;
										tag2.Restart = true;

										tag1.IsCollision = false;
										tag2.IsCollision = false;

										levelObjectsList.Clear();
										collisionList.Clear();

										if (clock == null)
										{
											clock = new DispatcherTimer();
											clock.Interval = new TimeSpan(0, 0, 0, 0, 40);
											clock.Start();
											clock.Tick += new EventHandler(clock_Tick);
											clock.Start();
										}
										else
										{
											clock.Start();
										}

										tag1.IsNextLevel = true;

										PlaySound(NextLevelSound);
									}
								}

								
								// If collision lost life, set Restart
								if ((tag1.LivesValue < 0) || (tag2.LivesValue < 0))
								{
									tag1.Restart = true;
									tag2.Restart = true;
								}
	
								foreach (TextBlock t in scoreList)
								{
									int value = 0;
									try
									{
										value = Int32.Parse(t.Text);
									} catch
									{
									}
									value += tag1.ScoreValue;
									value += tag2.ScoreValue;
									t.Text = value.ToString();
									lastScore = Int16.Parse(t.Text);
								}
								foreach (TextBlock t in livesList)
								{
									int value = 0;
									try
									{
										value = Int32.Parse(t.Text);
									}
									catch
									{
									}
									value += tag1.LivesValue;
									value += tag2.LivesValue;

									t.Text = value.ToString();

									// **********************************
									// GAME OVER
									// If lives < 0, GAME OVER
									// **********************************
									if (value < 0)
									{
										Level = 1;

										PlaySound(GameOverSound);

										foreach (TextBlock l in lastscoreList)
										{
											l.Text = lastScore.ToString();
										}

										VisualStateManager.GoToState(mainControl, GameOverState, true);
										foreach (TextBlock h in highscoreList)
										{
											if (Int16.Parse(h.Text) < lastScore)
											{
												h.Text = lastScore.ToString();
												SaveHighscore(lastScore.ToString());
											}
											foreach (TextBlock s in scoreList)
											{
												s.Text = "0000";
											}
										}
										t.Text = lives.ToString();
										VisualStateManager.GoToState(mainControl, "Reset", true);
										VisualStateManager.GoToState(mainControl, "Level1", true);

										tag1.IsNextLevel = false;
										tag1.IsGameOver = true;

										levelObjectsList.Clear();
										collisionList.Clear();

										if (clock == null)
										{
											clock = new DispatcherTimer();
											clock.Interval = new TimeSpan(0, 0, 0, 0, 40);
											clock.Start();
											clock.Tick += new EventHandler(clock_Tick);
											clock.Start();
										}
										else
										{
											clock.Start();
										}
									}
								}

								collideWithAllList[i].Tag = tag1;
								if (collisionList.Count >= (j-1)) collisionList[j].Tag = tag2;
							}
						}
					}
				}
		}

		// When a new Level starts, it's necessary to wait for VSM to finish setting all the new Properties
		void clock_Tick(object sender, EventArgs e)
		{
			UpdateListsNewLevel();
			clock.Stop();
		}


		#region Local Storage (High-score)

		private void SaveHighscore(string value)
		{
			IsolatedStorageFile score = IsolatedStorageFile.GetUserStoreForApplication();
			IsolatedStorageFileStream isfs = new IsolatedStorageFileStream("highscore.txt", FileMode.Create, score);
			StreamWriter streamWriter = new StreamWriter(isfs);
			streamWriter.WriteLine(value);
			streamWriter.Flush();
			streamWriter.Close();
		}

		private string ReadHighscore()
		{
			IsolatedStorageFile score = IsolatedStorageFile.GetUserStoreForApplication();
			string result;
			if (score.FileExists("highscore.txt"))
			{
				IsolatedStorageFileStream isfs = new IsolatedStorageFileStream("highscore.txt", FileMode.OpenOrCreate, score);
				StreamReader streamReader = new StreamReader(isfs);
				result = streamReader.ReadLine();
				streamReader.Close();
			}
			else
			{
				IsolatedStorageFileStream isfs = new IsolatedStorageFileStream("highscore.txt", FileMode.CreateNew, score);
				StreamWriter streamWriter = new StreamWriter(isfs);
				streamWriter.WriteLine("0");
				streamWriter.Flush();
				streamWriter.Close();
				result = "0";
			}
			return result;
		}

		#endregion


    }
}
