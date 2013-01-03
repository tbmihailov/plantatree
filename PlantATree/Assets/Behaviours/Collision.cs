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
using System.Windows.Threading;
using System.Windows.Interactivity;

namespace GamePack
{
    public class Collision : TriggerAction<DependencyObject>
    {
		public enum CollisionProperties { None, Visibility, GoToState, Motion_ChangeDirection };

		// Private Properties
		private FrameworkElement target;
		private UIElement _parent;
		public GameEnvironment game;
		Panel panel;

        #region Property to Expose


		[Category("Collision Properties")]
		public bool CollideWithAll { get { return (bool)GetValue(CollideWithAllProperty); } set { SetValue(CollideWithAllProperty, value); } }
		[Category("Collision Properties")]
		public bool IsMoving { get { return (bool)GetValue(IsMovingProperty); } set { SetValue(IsMovingProperty, value); } }
		[Category("Collision Properties")]
		public int Score { get { return (int)GetValue(ScoreProperty); } set { SetValue(ScoreProperty, value); } }
		[Category("Collision Properties")]
		public int Lives { get { return (int)GetValue(LivesProperty); } set { SetValue(LivesProperty, value); } }
		[Category("Collision Properties")]
		public System.Uri Audio { get { return (System.Uri)GetValue(AudioProperty); } set { SetValue(AudioProperty, value); } }

		[Category("Change Properties")]
		public CollisionProperties Action { get { return (CollisionProperties)GetValue(ActionProperty); } set { SetValue(ActionProperty, value); } }
		[Category("Change Properties")]
		public string Value { get { return (string)GetValue(ValueProperty); } set { SetValue(ValueProperty, value); } }

		public static readonly DependencyProperty ActionProperty = DependencyProperty.Register("Action", typeof(CollisionProperties), typeof(Collision), null);
		public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(string), typeof(Collision), null);
		public static readonly DependencyProperty CollideWithAllProperty = DependencyProperty.Register("CollideWithAll", typeof(bool), typeof(Collision), null);
		public static readonly DependencyProperty IsMovingProperty = DependencyProperty.Register("IsMoving", typeof(bool), typeof(Collision), null);
		public static readonly DependencyProperty ScoreProperty = DependencyProperty.Register("Score", typeof(int), typeof(Collision), null);
		public static readonly DependencyProperty LivesProperty = DependencyProperty.Register("Lives", typeof(int), typeof(Collision), null);
		public static readonly DependencyProperty AudioProperty = DependencyProperty.Register("Audio", typeof(System.Uri), typeof(Collision), null);


        #endregion

		protected override void OnAttached()
		{
			target = this.AssociatedObject as FrameworkElement;
			_parent = VisualTreeHelper.GetParent(target) as UIElement;

			// Updates tag object that sends information among game elements
			CollisionTag tag = new CollisionTag();
			tag.ScoreValue = Score;
			tag.LivesValue = Lives;
			if (CollideWithAll)
			{
				tag.CollisionType = "CollideWithAll";
			}
			else
			{
				tag.CollisionType = "Collision";
				if (Action == CollisionProperties.Visibility)
				{
					tag.IsLevelObject = true;
				}
				else
				{
					tag.IsLevelObject = false;
				}
			}
			if (IsMoving)
			{
				tag.IsMoving = true;
			}
			target.Tag = tag;

			// Starts Collision loop
			CompositionTarget.Rendering += new EventHandler(CollisionCheck);

			base.OnAttached();
		}

		void CollisionCheck(object sender, EventArgs e)
		{
			if (target.Visibility == Visibility.Visible)
			{
				CollisionTag tag = target.Tag as CollisionTag;
				if (tag != null)
				{
					// If Environment set tag IsCollision to TRUE, it means that this element had collision
					if ((tag.IsCollision == true))
					{
						CollisionTag newTag = new CollisionTag();

						// The collision action depends on the value of the Dependency Property "Action"
						switch (Action)
						{
							// If Action is Visibility, hide or show the element
							case CollisionProperties.Visibility:
								switch (Value)
								{
									case "Visible":
										target.Visibility = Visibility.Visible;
										break;
									case "Collapsed":
										target.Visibility = Visibility.Collapsed;
										break;
								}
								newTag.IsChangeMotion = false;
								newTag.IsCollision = false;
								newTag.Normal = null;
								break;
							// If Action is GoToState, trigger VSM state called "IsCollided"
							case CollisionProperties.GoToState:
								VisualStateManager.GoToState(this.AssociatedObject as Control, "IsCollided", true);
								//newTag.CollisionType = "Hidden";
                                Control me = this.AssociatedObject as Control;
                                me.Visibility = Visibility.Collapsed;
								newTag.IsChangeMotion = false;
								newTag.IsCollision = false;
								newTag.Normal = tag.Normal;
								break;
							// If Action is Motion_ChangeDirection, set tag IsChangeMotion to be used inside the Action "Motion"
							case CollisionProperties.Motion_ChangeDirection:
								newTag.IsChangeMotion = true;
								newTag.IsCollision = false;
								newTag.Normal = tag.Normal;
								break;
							default:
								newTag.IsChangeMotion = false;
								newTag.IsCollision = false;
								newTag.Normal = tag.Normal;
								break;
						}

						// Copy all tags from the new tag object, so information doesn't get lost
						newTag.CollisionType = tag.CollisionType;
						newTag.Restart = tag.Restart;
						newTag.IsGameOver = tag.IsGameOver;
						newTag.ScoreValue = tag.ScoreValue;
						newTag.LivesValue = tag.LivesValue;
						newTag.IsLevelObject = tag.IsLevelObject;
						newTag.IsNextLevel = tag.IsNextLevel;
						newTag.IsMoving = tag.IsMoving;
						newTag.CollisionX = tag.CollisionX;
						newTag.CollisionCenter = tag.CollisionCenter;
						target.Tag = newTag;

						// Play Audio
						if ((panel != null) && (Audio != null))
						{
							MediaElement audio = new MediaElement();
							audio.Source = Audio;
							audio.MediaEnded += delegate
							{
								panel.Children.Remove(audio);
							};

							audio.MediaFailed += this.HandleError;

							panel.Children.Add(audio);
							audio.Position = new TimeSpan(0, 0, 0, 0, 0);
							audio.Play();
						}
					}
				}
			}
		}

		protected override void Invoke(object o)
		{
			panel = this.Panel;
		}


		private Panel Panel
		{
			get
			{
				FrameworkElement parent = this.AssociatedObject as FrameworkElement;
				while (parent != null)
				{
					if (parent is Grid || parent is Canvas)
						return (Panel)parent;

					parent = VisualTreeHelper.GetParent(parent) as FrameworkElement;
				}
				return null;
			}
		}

		private void HandleError(object sender, ExceptionRoutedEventArgs e)
		{
		}

    }
}
