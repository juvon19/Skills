using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pirates;

namespace MyBot
{
    /// <summary>
    /// This is an example for a bot.
    /// </summary>
    /// 
    public class TutorialBot : IPirateBot
    {

        /// <summary>
        /// Makes the bot run a single turn.
        /// </summary>
        /// <param name="game">The current game state</param>
        public void DoTurn(PirateGame game)
        {
            HandlePirates(game);
        }
        public void HandlePirates(PirateGame game)
        {
            foreach (Pirate pirate in game.GetMyLivingPirates())
            {
                if (!TryPush(pirate, game))
                {

                    if (game.GetMyCapsule().Holder == null) //WE HAVE NO CAPSULE
                    {
                        if (pirate == xClosestToY(game.GetMyCapsule(), game.GetMyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate)
                        {
                            pirate.Sail(game.GetMyCapsule().InitialLocation);
                        }
                        else if (game.GetEnemyCapsule().Holder == null) //WE DONT HAVE, ENEMY DONT HAVE CAPSULE
                        {
                            System.Console.WriteLine($"STATE: WE DONT HAVE, ENEMY DONT HAVE");
                            if (pirate == xClosestToY(game.GetMyCapsule(), game.GetMyLivingPirates().Cast<GameObject>().ToList())[1] as Pirate)
                            {
                                Pirate first = xClosestToY(game.GetMyCapsule(), game.GetMyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate;
                                pirate.Sail(first);
                            }
                            // SENDS CLOSEST PIRATE 1&2 TO- CLOSEST ENEMY TO CLOSEST ENEMY CAPSULE
                            else if (xClosestToY(xClosestToY(game.GetEnemyCapsule().InitialLocation, game.GetEnemyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate, game.GetMyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate == pirate || xClosestToY(xClosestToY(game.GetEnemyCapsule().InitialLocation, game.GetEnemyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate, game.GetMyLivingPirates().Cast<GameObject>().ToList())[1] as Pirate == pirate)
                            {
                                pirate.Sail(xClosestToY(game.GetEnemyCapsule().InitialLocation, game.GetEnemyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate);
                            }
                            else
                            {
                                pirate.Sail(xClosestToY(xClosestToY(game.GetMyCapsule(), game.GetMyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate, game.GetEnemyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate);
                            }

                        }
                        else //WE NO HAVE, ENEMY HAVE CAPSULE
                        {
                            System.Console.WriteLine($"STATE: WE DONT HAVE, ENEMY HAVE");
                            if (pirate == xClosestToY(game.GetMyCapsule(), game.GetMyLivingPirates().Cast<GameObject>().ToList())[1] as Pirate)
                            {
                                Pirate first = xClosestToY(game.GetMyCapsule(), game.GetMyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate;
                                pirate.Sail(first);
                            }
                            else
                            {
                                pirate.Sail(game.GetEnemyCapsule().Holder);
                            }
                        }
                    }
                    // SEMI DONE
                    else //WE HAVE CAPSULE
                    {
                        if (pirate.HasCapsule())
                        {
                            pirate.Sail(game.GetMyMothership());
                        }
                        else if (game.GetEnemyCapsule().Holder == null) //WE HAVE, ENEMY DONT HAVE CAPSULE
                        {
                            System.Console.WriteLine($"STATE: WE HAVE, ENEMY DONT HAVE");

                            // IF SHAMEN CHECKS IF A PIRATE IS THE CLOSEST TO INITIAL OF CAPSULE AND PERFECT ROTATION POSSIBLE and CHECKS FOR CLOSEST PIRATE
                            if (game.GetMyCapsule().Holder.Distance(game.GetMyMothership()) + game.GetMyMothership().Distance(game.GetMyCapsule().InitialLocation) > game.GetMyCapsule().InitialLocation.Distance(xClosestToY(game.GetMyCapsule().InitialLocation, game.GetMyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate) && pirate == xClosestToY(game.GetMyMothership(), game.GetMyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate)
                            {
                                pirate.Sail(game.GetMyCapsule().InitialLocation);
                            }
                            else if (xClosestToY(game.GetMyCapsule().Holder, game.GetMyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate == pirate)
                            {
                                pirate.Sail(game.GetMyCapsule().Holder);
                            }
                            // SENDS CLOSEST PIRATE 1&2 TO- CLOSEST ENEMY TO CLOSEST ENEMY CAPSULE
                            else if (xClosestToY(xClosestToY(game.GetEnemyCapsule().InitialLocation, game.GetEnemyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate, game.GetMyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate == pirate || xClosestToY(xClosestToY(game.GetEnemyCapsule().InitialLocation, game.GetEnemyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate, game.GetMyLivingPirates().Cast<GameObject>().ToList())[1] as Pirate == pirate)
                            {
                                pirate.Sail(xClosestToY(game.GetEnemyCapsule().InitialLocation, game.GetEnemyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate);
                            }
                            else // ALL OTHER PIRATES (NOT DONE BITCHLAL)
                            {
                                pirate.Sail(xClosestToY(xClosestToY(game.GetMyCapsule(), game.GetMyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate, game.GetEnemyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate);
                            }

                        }
                        else //WE HAVE, ENEMY HAVE CAPSULE
                        {
                            System.Console.WriteLine($"STATE: WE HAVE, ENEMY HAVE");
                            // IF SHAMEN CHECKS IF A PIRATE IS THE CLOSEST TO INITIAL OF CAPSULE AND PERFECT ROTATION POSSIBLE and CHECKS FOR CLOSEST PIRATE
                            if (game.GetMyCapsule().Holder.Distance(game.GetMyMothership()) + game.GetMyMothership().Distance(game.GetMyCapsule().InitialLocation) > game.GetMyCapsule().InitialLocation.Distance(xClosestToY(game.GetMyCapsule().InitialLocation, game.GetMyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate) && pirate == xClosestToY(game.GetMyMothership(), game.GetMyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate)
                            {
                                pirate.Sail(game.GetMyCapsule().InitialLocation);
                            }
                            else if (xClosestToY(game.GetMyCapsule().Holder, game.GetMyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate == pirate)
                            {
                                pirate.Sail(game.GetMyCapsule().Holder);
                            }
                            // CHECKS IF THE PIRATE IS CLOSE TO HOLDER IF HE DOES AND WE HAVE LESS PIRATES THAN THEM PIRATE GOES TO DEFEND OR SOMETHING
                            else if (EnemyCloseToOurCapsule(game) > 1 && pirate.Distance(game.GetMyCapsule().Holder) < 900 && pirate != game.GetMyCapsule().Holder && AllyCloseToOurCapsule(game) + 1 < EnemyCloseToOurCapsule(game))
                            {
                                pirate.Sail(game.GetMyCapsule().Holder);
                            }
                            // SENDS THE 1st & 2nd CLOSEST PIRATES TO ENEMY CAPSULE
                            else if (xClosestToY(game.GetEnemyCapsule().Holder, game.GetMyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate == pirate || xClosestToY(game.GetEnemyCapsule().Holder, game.GetMyLivingPirates().Cast<GameObject>().ToList())[1] as Pirate == pirate)
                            {
                                pirate.Sail(game.GetEnemyCapsule().Holder);
                            }
                            // GOES TO ENEMY MOTHERSHIP IN ORDER TO DEFEND
                            else if (game.GetEnemyCapsule().Holder.InRange(game.GetEnemyMothership(), 1500) && pirate.InRange(game.GetEnemyMothership(), 1200))
                            {
                                pirate.Sail(game.GetEnemyCapsule().Holder);
                            }
                            // CHECKS IF ENEMY PIRATE IS IN RANGE OF OUR MS, IF SO, CLOSEST PIRATE TO THIS ENEMY WILL GO TO HIM
                            else if ((xClosestToY(game.GetMyMothership(), game.GetEnemyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate).InRange(game.GetMyMothership(), 800) && xClosestToY((xClosestToY(game.GetMyMothership(), game.GetEnemyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate), game.GetMyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate == pirate)
                            {
                                pirate.Sail((xClosestToY(game.GetMyMothership(), game.GetEnemyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate));
                            }
                            // LETS SAIL TO CLOSEST ENEMY PIRATE AND FUCK HIN IN THE ASS HOLEEEEE !Q!!:)
                            else
                            {
                                pirate.Sail(xClosestToY(pirate, game.GetEnemyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate);
                            }

                        }
                    }
                }
            }
        }

        /// <summary>
        /// Makes the pirate try to push an enemy pirate. Returns true if it did.
        /// </summary>
        /// <param name="pirate">The pushing pirate.</param>
        /// <param name="game">The current game state.</param>
        /// <returns>true if the pirate pushed.</returns>
        private bool TryPush(Pirate pirate, PirateGame game)
        {

            foreach (Pirate ally in game.GetMyLivingPirates())
            {
                // IF WE HAVE CAP AND PIRATE IS THE CAPSULE HOLDER RETURN FALSE
                if (game.GetMyCapsule().Holder != null && game.GetMyCapsule().Holder == pirate)
                    return false;
                // IF WE HAVE CAP AND PIRATE CAN PUSH CAP HOLDER AND HE IS THE CLOSEST TO HIM WE RETURN TRUE
                if (game.GetMyCapsule().Holder != null && ally.CanPush(game.GetMyCapsule().Holder) && PiratesInRangeOfPush(game, game.GetMyCapsule().Holder)[0] == pirate)
                {
                    ally.Push(game.GetMyCapsule().Holder, game.GetMyMothership());
                    System.Console.WriteLine($"pirate:{ally} did push capholder: {game.GetMyCapsule().Holder} to MOTHERSHIP");
                    return true;
                }
                // IF WE DONT HAVE CAPSULE AND WE CAN PUSH THE CLOSEST PIRATE TO CAP LOCATION THAN DO IT!
                if (game.GetMyCapsule().Holder == null && ally.CanPush(xClosestToY(game.GetMyCapsule().InitialLocation, game.GetMyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate))
                {
                    if ((xClosestToY(game.GetMyCapsule().InitialLocation, game.GetMyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate).Distance(game.GetMyCapsule().InitialLocation) >= game.PirateMaxSpeed * 3 && ally.CanPush(xClosestToY(game.GetMyCapsule().InitialLocation, game.GetMyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate) && xClosestToY(game.GetMyCapsule().InitialLocation, game.GetMyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate == ally)
                    {
                        ally.Push(xClosestToY(game.GetMyCapsule().InitialLocation, game.GetMyLivingPirates().Cast<GameObject>().ToList())[0] as Pirate, game.GetMyCapsule().InitialLocation);
                        System.Console.WriteLine($"pirate:{ally} did push closest to cap: {game.GetMyCapsule().Holder} to CAP");
                        return true;
                    }
                }

            }

            //WE PREFFER TO PUSH OURSELVES FIRST!
            foreach (Pirate enemy in game.GetEnemyLivingPirates())
            {
                //TODO :  IF TIGBORET PUSH HIM TO BUY SOME TIME

                // IF ENEMY HAS CAP AND WE HAVE MORE THAN 2 TO DROP HIS CAP
                if (game.GetEnemyCapsule().Holder != null && enemy == game.GetEnemyCapsule().Holder && PiratesInRangeOfPush(game, enemy).Count > 1 && pirate.CanPush(enemy))
                {
                    pirate.Push(enemy, EnemyCanExplode(game, enemy));
                    return true;
                }
                // IF ENEMY CAN GO OUT OF MAP KICK HIM!
                if (!EnemyCanExplode(game, enemy).Equals(enemy.InitialLocation.Add(enemy.InitialLocation)))
                {
                    if (isOneFromThisList(game, PiratesInRangeOfPush(game, enemy), pirate))
                    {
                        pirate.Push(enemy, EnemyCanExplode(game, enemy));
                        return true;
                    }
                }



                // Check if the pirate can push the enemy.
                /*if (pirate.CanPush(enemy))
                {
                    // Push enemy!
                    //***POSSIBLE BUG***
                    pirate.Push(enemy, EnemyCanExplode(game, enemy));

                    // Print a message.
                    System.Console.WriteLine("pirate " + pirate + " pushes " + enemy + " towards " + EnemyCanExplode(game, enemy));

                    // Did push.
                    return true;
                }*/
            }

            // Didn't push.
            return false;
        }

        public int EnemyPirateNearEnemyCapsule(PirateGame game)
        {
            return game.GetEnemyLivingPirates().Count(pirate => pirate.InRange(game.GetEnemyCapsule(), 500));
        }

        public int EnemyCloseToOurCapsule(PirateGame game)
        {
            return game.GetEnemyLivingPirates().Count(enemy => enemy.InRange(game.GetMyCapsule(), 800));
        }
        public int AllyCloseToOurCapsule(PirateGame game)
        {
            return game.GetMyLivingPirates().Count(pirate => pirate.InRange(game.GetMyCapsule(), 800));
        }

        public List<Pirate> PiratesInRangeOfPush(PirateGame game, Pirate pirate)
        {
            List<Pirate> list = game.GetMyLivingPirates().Where(pir => pir.CanPush(pirate) && pir.PushReloadTurns == 0).ToList();
            return xClosestToY(pirate, list.Cast<GameObject>().ToList()).Cast<Pirate>().ToList();
        }


        //FINDS THE LOCATION POSSIBLE TO PUSH ENEMY OUT OF MAP BOUNDS OTHERWISE INIT LOCATION
        public Location EnemyCanExplode(PirateGame game, Pirate enemy)
        {
            int count = PiratesInRangeOfPush(game, enemy).Count;
            int col = enemy.Location.Col;
            int row = enemy.Location.Row;
            //ROWS
            if (row + (count * game.PushDistance) > game.Rows)
                return new Location(row + (count * game.PushDistance), col);
            if (row - (count * game.PushDistance) < 0)
                return new Location(row - (count * game.PushDistance), col);
            //COLS
            if (col + (count * game.PushDistance) > game.Cols)
                return new Location(row, col + (count * game.PushDistance));
            if (col - (count * game.PushDistance) < 0)
                return new Location(row, col - (count * game.PushDistance));
            //CANT PUSH OUT OF MAP, SEND TO INITIAL LOCATION
            return enemy.InitialLocation.Add(enemy.InitialLocation);
        }

        public bool isOneFromThisList(PirateGame game, List<Pirate> list, Pirate pirate)
        {
            foreach (Pirate item in list)
            {
                if (item.Equals(pirate))
                    return true;
            }
            return false;
        }

        public List<GameObject> xClosestToY(MapObject x, List<GameObject> y)
        {
        }

    }
}