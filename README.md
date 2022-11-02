My thoughts on # Hungry-Shark

Actually, it took me a while to find suitable 3D models and figure out how to import them into Unity. Usually I was given art assets and asked to add code on it. 
So this was a quite novel and fun experience for me.
Since our targeting players are so young, I don't want the game mechanics to be very hard. Basic collision detection to earn scores should be good enough and 
I believe that the key focus should be on designing how the fish swims. Where are they spawned? What path do they take? How fast do they swim? Do they have any 
attractive graphcs that kids like. Very sadly, I don't think I have time to implement this part well. Currently the fish just swim straight across the screen.

Kids are interested in moving objects, so I plan to let the fish swim across the screen very quickly and let kids to catch them. To make it more challening, the sharks
should not be allowed to move freely, which means Input.GetKeyDown() is used, one key press pushes it forward a bit and there are cool-down time between each key press.

The following are a few programming patterns I impltemented in the game:
The fish spwans randomly from three pre-defined locations and object pool was used.
Commaned pattern was implemented for moving the shark by key press.
Observer pattern was implemented in many places for event trigger, such as onFishDie event.
Single pattern was planed to implemented for ScoreManger, but unfortunately not finished. So I decided to remove it in the end.

Future impveoment:
Instead of swimming straight across, fish can swim along a curve, implemented using Bezier curve.
A better start scene with instructions on how to play and a smooth scene transition.
More complex and challenging gameplay. There should be some item hidded somewhere in the sea. Once the shark eats an super power candy, it can swim faster and freely in any direction (notn just left/right/up/down)..
The fish should also swim faster as time flows by.
More beautiful graphical style, including particle system and post-processing effects.
The current UI is quite ugly.
