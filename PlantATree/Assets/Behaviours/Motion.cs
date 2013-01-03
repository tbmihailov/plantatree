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
using System.Windows.Interactivity;

// Action that should be attached to the Element that moves by itself, like the Ball

namespace GamePack
{
	public class Vector
	{
		public double X { get; set; }
		public double Y { get; set; }

		public Vector(double x, double y)
		{
			X = x;
			Y = y;
		}
		public Vector()
		{
		}
		public double Length()
		{
			return Math.Sqrt(Math.Pow(X, 2) + Math.Pow(Y, 2));
		}


		#region  Vector Operators

		public static Vector operator +(Vector v1, Vector v2)
		{
			return new Vector(v1.X + v2.X, v1.Y + v2.Y);
		}
		public static Vector operator +(Vector v1, Point p)
		{
			return new Vector(v1.X + p.X, v1.Y + p.Y);
		}
		public static Vector operator -(Vector v1, Vector v2)
		{
			return new Vector(v1.X - v2.X, v1.Y - v2.Y);
		}
		public static Vector operator -(Vector v1, Point p)
		{
			return new Vector(v1.X - p.X, v1.Y - p.Y);
		}
		public static Vector operator /(Vector v1, double d)
		{
			return new Vector(v1.X / d, v1.Y / d);
		}
		public static Vector operator *(Vector v1, double m)
		{
			return new Vector(v1.X * m, v1.Y * m);
		}

		#endregion


		public Vector Angle(Vector v1, Vector v2)
		{
			return null;
		}

	}


    public class Motion : TriggerAction<DependencyObject>
    {
		// Private Properties
		private FrameworkElement target;
		private UIElement _parent;
		private double vo = 0;
		private double X = 0;
		private double Y = 0;
		private double startX, startY;
		private double startDirection;
		private double startSpeed;
		private RotateTransform rotate = new RotateTransform();

		private Control mainControl;

        #region Dependency Properties

        [Category("Motion Properties")]
		public double Speed { get { return (double)GetValue(SpeedProperty); } set { SetValue(SpeedProperty, value); } }
		[Category("Motion Properties")]
		public double Direction { get { return (double)GetValue(DirectionProperty); } set { SetValue(DirectionProperty, value); } }
		[Category("Motion Properties")]
		public bool AutoStart { get { return (bool)GetValue(AutoStartProperty); } set { SetValue(AutoStartProperty, value); } }
		[Category("MotionAdvanced Properties")]
		public bool Rotates { get { return (bool)GetValue(RotatesProperty); } set { SetValue(RotatesProperty, value); } }

		public static readonly DependencyProperty SpeedProperty = DependencyProperty.Register("Speed", typeof(double), typeof(Motion), new PropertyMetadata(null));
		public static readonly DependencyProperty DirectionProperty = DependencyProperty.Register("Direction", typeof(double), typeof(Motion), new PropertyMetadata(null));
		public static readonly DependencyProperty RotatesProperty = DependencyProperty.Register("Rotates", typeof(bool), typeof(Motion), new PropertyMetadata(null));
		public static readonly DependencyProperty AutoStartProperty = DependencyProperty.Register("AutoStart", typeof(bool), typeof(Motion), new PropertyMetadata(null));

        #endregion


		protected override void OnAttached()
		{
			base.OnAttached();
			target = this.AssociatedObject as FrameworkElement;
			_parent = VisualTreeHelper.GetParent(target) as UIElement;
			
		}

        protected override void Invoke(object o)
        {
			DependencyObject parent = this.AssociatedObject as DependencyObject;
			while (parent != null)
			{
				mainControl = parent as Control;
				parent = VisualTreeHelper.GetParent(parent) as DependencyObject;
			}

			if (AutoStart)
				vo = Speed;
			else
				vo = 0;

			// Keep the values at the begining of the Game
			startSpeed = Speed;
			startX = Canvas.GetLeft(target);
			startY = Canvas.GetTop(target);
			startDirection = Direction;

			X = startX;
			Y = startY;

			target.MouseLeftButtonUp += new MouseButtonEventHandler(target_MouseLeftButtonUp);

			// Starts Motion loop
			CompositionTarget.Rendering += new EventHandler(MotionRender);
        }

		// If Mouse Up, then start game
		void target_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
		{
			if (vo == 0) vo = Speed;
			VisualStateManager.GoToState(mainControl, "StartGame", true);
		}

		void MotionRender(object sender, EventArgs e)
		{
			double vx = vo * Math.Cos(Direction * Math.PI / 180);
			double vy = vo * Math.Sin(Direction * Math.PI / 180);

			CollisionTag tag = target.Tag as CollisionTag;
			CollisionTag newTag = new CollisionTag();

			// Increase Speed each new Level
			if (tag.IsNextLevel)
			{
				++Speed;
			}

			if (tag.Restart)
			{
				// If life is lost, restart Motion
				vo = 0;
				X = startX;
				Y = startY;
				if (tag.IsGameOver) Speed = startSpeed;
				Direction = startDirection;
				newTag.Normal = new Vector(1, 1);
				newTag.IsChangeMotion = false;
				newTag.IsCollision = false;
				newTag.Restart = false;
				target.Tag = newTag;
			}
			else
			{
				// If life is not lost, keep Motion as is
				if (tag != null)
				{
					// If collided with a moving Paddle
					if (tag.IsMoving)
					{
						tag.IsMoving = false;

						double ballX = Canvas.GetLeft(target);
						double padX = tag.CollisionX;
						tag.CollisionX = 0;

						double centerPad = tag.CollisionCenter;
						double centerBall = target.Width / 2; 
						tag.CollisionCenter = 0;

						double angle;
						angle = (ballX + centerBall) - (padX + centerPad);

						vx = (angle) / 8;
						vy = Math.Abs(vy) * (-1);

						newTag.Normal = new Vector(1, 1);
						newTag.IsChangeMotion = false;
						newTag.IsCollision = false;
						target.Tag = newTag;
						Direction = Math.Atan2(vy, vx) * 180 / Math.PI;

					}
					else
					{
						// If collided with a fixed element
						if (tag.IsChangeMotion == true)
						{
							if (tag.Normal.X < 0)
							{
								if (vx < 0)
								{
									X += Math.Abs(vx);
									Canvas.SetLeft(target, X);
								}
								else
								{
									X -= Math.Abs(vx);
									Canvas.SetLeft(target, X);
								}
							}
							if (tag.Normal.Y < 0)
							{
								if (vy < 0)
								{
									Y += Math.Abs(vy);
									Canvas.SetTop(target, Y);
								}
								else
								{
									Y -= Math.Abs(vy);
									Canvas.SetTop(target, Y);
								}
							}
							vx *= tag.Normal.X;
							vy *= tag.Normal.Y;

							newTag.Normal = new Vector(1, 1);
							newTag.IsChangeMotion = false;
							newTag.IsCollision = false;
							target.Tag = newTag;
							Direction = Math.Atan2(vy, vx) * 180 / Math.PI;
						}
					}
				}
				X += vx;
				Y += vy;
			}

			// Updates position
			Canvas.SetLeft(target, X);
			Canvas.SetTop(target, Y);

			if (Rotates)
			{
				rotate.Angle = Direction;
				target.RenderTransform = rotate;
			}
		}
    }
}
