# object-follower
**A unity package for simple object following**

This package currently contains only a simple MonoBehaviour script (with a custom editor) which is used for tracking/following objects. In the future new features will be added one of those is a UI object follower.

**usage**

1. Add the Object Follower component to the object that is going to follow the other object.
2. Drag the object to follow in the *Target* field.
3. Choose the aspect of the transform to follow/copy
4. Enjoy :)

**examples of usage**

This package can be used to make the hierarchy of a character easier to read. For example instead of
childing an axe to the palm of a character which is several levels down the hierarchy you put the script
on the axe and set it to follow the palm.