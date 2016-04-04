# MSP-Sphero-Pilot--Aurora-Catch

###Description:

Aurora Catch is a new way to play catch. Unlike normal catch games it requires two people, but with the Sphero you can do this with just a single person and the Sphero. When you roll a ball the ball does not role back to you, but Aurora Catch changes that. With Aurora Catch you will be able to roll the Sphero across the room and it will automatically roll right back to you!




###How Does it Work?

With Sphero's sensors (accelerometer & gyroscope) we are able to take advantage of this information and do a lot of cool things with it. I will be able to track Sphero's position so if it is rolled and hits a wall and changes course we are able to understand this information and when Sphero needs to roll back our program can roll it back even if it had turned a few times.




###Week 2 Update:

So the initial part of this update was getting my app to connect to the Sphero and understanding how the Sphero functions such as setting the heading, accelerometer, and gyroscope values. I have started to code the layout of the app in XAML and implementing the backend C# code to switch pages in the SplitView pane.

I have also started to do some tests rolling the Sphero across the floor and my results where not the ones I had wished. Upon rolling the Sphero it did not roll smoothly, but instead bumpy and also it did not roll straight at all. After some thinking I came up with this. The Sphero keeps its heading always at the same spot when rotating it. So if I am able to keep track of these movements when the ball is in your hand I should be able to align it accordingly so that the heading is set to forward that corresponds to the way you would roll it. From there I will take the accelerometer values and measure how much force is applied upon rolling and multiply that from test I will do to indicate how much force it requires to go a certain distance. Finally since the Sphero has a counter weight (this was what was causing the ball to appear as it is driving over bumps) I will have the Sphero actually drive to make it appear as if it is rolling.




###Week 3 Update:
I had originally wanted to be able to roll Sphero in any direction, but I had encountered issues since Sphero is always keeping it's heading at the same location upon rotation of the ball. With the Sphero API we are given access to an attitude property which would give us the following: pitch, row, and yaw. I would be most interested in the yaw since that gives me the rotation and I could update the heading. I was
