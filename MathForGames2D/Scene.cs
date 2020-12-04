using System;
using System.Collections.Generic;
using System.Text;
using MathLibrary;
using Raylib_cs;

namespace MathForGames2D
{
    class Scene
    {
        //Sets teh variables
        private Actor[] _actors;
        private Matrix3 _transform = new Matrix3();

        //Sets a world in scenes
        public Matrix3 World
        {
            get { return _transform; }
        }

        //Gets the start and sets it private
        public bool Started { get; private set; }

        //Add new scene
        public Scene()
        {
            _actors = new Actor[0];
        }

        //Adds new Actor to scene
        public void AddActor(Actor actor)
        {
            //Creating a new array with a size one greater than our old array
            Actor[] appendedArray = new Actor[_actors.Length + 1];
            //Copy the values from the old array to the new array
            for (int i = 0; i < _actors.Length; i++)
            {
                appendedArray[i] = _actors[i];
            }
            //Set the last value in the new array to be the actor we want to add
            appendedArray[_actors.Length] = actor;
            //Set old array to hold values of the new array
            _actors = appendedArray;
        }

        //Removes actor from scenes
        public bool RemoveActor(int index)
        {
            //Check to see if the index is outside the bounds of our array
            if (index < 0 || index >= _actors.Length)
            {
                return false;
            }

            bool actorRemoved = false;

            //Create a new array wtih a size one less than our old array
            Actor[] newArray = new Actor[_actors.Length - 1];
            //Create variable to access tempArray index
            int j = 0;
            //Copy values from the old array to the new array
            for (int i = 0; i < _actors.Length; i++)
            {
                //If the current index is not the index that needs to be removed,
                //add the value into the old array and increment j
                if (i != index)
                {
                    newArray[j] = _actors[i];
                    j++;
                }
                else
                {
                    actorRemoved = true;
                    if (_actors[i].Started)
                        _actors[i].End();
                }

            }
            //Set the old array to be the tempArray
            _actors = newArray;
            return actorRemoved;
        }

        //Removes the actor from scene
        public bool RemoveActor(Actor actor)
        {
            //Check to see if the actor was null
            if (actor == null)
            {
                return false;
            }

            bool actorRemoved = false;

            Actor[] newArray = new Actor[_actors.Length - 1];

            int j = 0;

            for (int i = 0; i < _actors.Length; i++)
            {
                if (actor != _actors[i])
                {
                    newArray[j] = _actors[i];
                    j++;
                }
                else
                {
                    actorRemoved = true;
                    if (actor.Started)
                        actor.End();
                }
            }
            //Set the old array to the new array
            _actors = newArray;
            //Return whether or not the removal was successful
            return actorRemoved;
        }

        //Checks collision in scene
        private void CheckCollision()
        {
            foreach (Actor actor in _actors)
            {
                foreach (Actor actor1 in _actors)
                {

                    if (actor == actor1)continue;

                    if (actor is Background || actor1 is Background) continue;

                    if (actor.CollisionRadius + actor1.CollisionRadius >= (actor.LocalPosition - actor1.LocalPosition).Magnitude)
                    {
                        actor.OnCollision(actor1);
                    }
                }
            }

            
        }

        //Starts the scenes
        public virtual void Start()
        {
            Started = true;
        }

        //Updates scenes
        public virtual void Update(float deltaTime)
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                if (!_actors[i].Started)
                    _actors[i].Start();

                _actors[i].Update(deltaTime);
            }
            CheckCollision();
        }

        //Draws Scene
        public virtual void Draw()
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                _actors[i].Draw();
            }
        }

        //Ends scene
        public virtual void End()
        {
            for (int i = 0; i < _actors.Length; i++)
            {
                if (_actors[i].Started)
                    _actors[i].End();
            }
            Started = false;

        }
    }
}
