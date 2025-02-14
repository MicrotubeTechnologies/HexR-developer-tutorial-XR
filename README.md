# HexR Unity Integration (Uses Open XR) ℹ️

## Installation

### Prerequisites:
- Ensure you are using **Unity 2021.3.26f1** or newer.
- For projects using **Meta OVR**, refer to the official [HexR Developer Tutorial (Meta OVR)](https://github.com/MicrotubeTechnologies/HexR-Developer-Tutorial-Meta-OVR).
  
### Steps to Get Started:
1. **Clone this repository:**
   https://github.com/MicrotubeTechnologies/HexR-developer-tutorial-XR

2. **Open the HexR Developer Tutorial project in Unity.**

---

<details>
  <summary>🔍 HexR Code Structure</summary>

### Learn more about the HexR code structure and architecture 💡

<details>
  <summary>1. Hand Tracking (PhysicsHandTracking)</summary>

#### The HexR hand supports both the **OpenXR** and **Meta OVR** hand skeleton structure.  
Here’s a summary of the differences in hand structure:
- **OpenXR Hand Skeleton**
- **Meta OVR Hand Skeleton**  
The `PhysicsHandTracking` script mimics the position/rotation of either the OpenXR or Meta OVR hands, the script is attached to the Left/Right hand physics component under HexR Main.

![Hand Skeleton](https://github.com/user-attachments/assets/2585a044-ae44-4814-88e5-abe61c876f8e)

If a custom hand structure is used, you will have to recreate the `PhysicsHandTracking` to track each joint.

</details>

<details>
  <summary>2. HexR Overall Manager (HaptGloveManager)</summary>

#### The `HaptGloveManager` simplifies the setup process.  
- In the inspector, ensure the XR framework is set to OpenXR and click the **"Auto Set Up HexR"** button.
- If Set up is successfull, there should be no missing links in the inspector for HexR main, Left Hand Physics and Right hand Physics.
- Check the debug log to ensure the setup is successful. 

![Setup Image](https://github.com/user-attachments/assets/f09f713f-fa81-484e-8646-bbe830ecce35)

#### HaptGloveManager Settings:
- **XR Framework:**  
  - Do select only the OpenXR Framework as there will be missing assets if meta OVR is selected, for projects using Meta OVR refer to the meta developer tutorial in the link above.

- **HexR Hand Menu:**  
  - The hand menu 
  
</details>

<details>
  <summary>3. Haptics Controller (PressureTrackerMain)</summary>

#### The `PressureTrackerMain` script contains all of the functions to trigger haptics.
#### There is 6 Channels in the HexR glove allowing haptics to be triggered for each finger and the palm

- Overview
  - Functions are categorized by **single-channel** or **multi-channel** triggers.
  - Haptics intensity range from 0.1 (no haptics) to 1 (Max haptics).
  - Refer to the demo scene to see examples of how these functions are used.

- Function : IsHandNear()
  - This is use to check if the user left or right hand is grabbing or near the target object, so that haptics is correctly triggered at the right timme and by the right hand.
    
- Function : CustomSingleHaptics ( Haptics.Finger finger, bool states, float intensity, float speed, bool ByPassHandCheck )
  - Haptics.Finger = which finger is to be triggered: index,middle,ring,pinky,thumb,palm
  - states : true = haptics in , false = haptics out
  - intensity : 0.1 - 1 , min haptics - max haptics
  - speed : 0.1 - 1 , slowly increase haptics vs fast increase haptics
  - ByPassHandCheck : true = will trigger haptics without checking IsHandNear()

- Function : CustomSingleVibrations(Haptics.Finger finger, bool states, float intensity, float frequency,float PeakRatio,float Speed,float endIntensity, bool ByPassHandCheck)
  - Haptics.Finger = which finger is to be triggered: index,middle,ring,pinky,thumb,palm
  - states : true = haptics in , false = haptics out
  - frequency : 0.1 - 2 
  - intensity : 0.1 - 1 , min haptics - max haptics
  - peakratio : 0.2 - 0.8
  - speed : 0.1 - 1 , slowly increase haptics vs fast increase haptics
  - endIntensity : vibrations to end with selected haptics pressure
  - ByPassHandCheck : true = will trigger haptics without checking IsHandNear()
</details>

<details>
  <summary>4. HexR Grab and Pinch (HexRGrabbable)</summary>

#### The `HexRGrabbable` script enables objects to be picked up by the HexR hands.
#### This is optional as you can also use the grab/pinch provided by **OpenXR**, however the haptics trigger and physics of grab will be different. Give both a try to see which is more suitable for you.
To set up `HexRGrabbable`:
1. Ensure the object has a **Collider (Trigger)** and **Rigidbody** attached to the same GameObject.
2. Since the interaction is physics-based, adjust the size of the collider to improve grab/pinch behavior.
3. Optionally, attach an additional collider if you want the object to interact with other GameObjects.

![Grabbable Example](https://github.com/user-attachments/assets/3fadad3e-80d7-4f57-9186-a63d4ebc125f)

#### HexRGrabbable Settings:
- **Type of Grab:**  
  - **Palm Grab:** Requires the palm and at least one finger to touch the object (thumb not required).
  - **Pinch Grab:** Requires the thumb and at least one finger to touch the object (palm not required).

- **Gravity Bool:**  
  If enabled, gravity will affect the object when released.

- **Haptic Slider:**  
  Controls the strength of the haptic feedback during grab or pinch.  
  - `0`: No haptics  
  - `60`: Maximum haptics strength

- **On Grab Event:**  
  Trigger an event when the object is grabbed or pinched.

- **On Release Event:**  
  Trigger an event when the object is released.

</details>

<details>
  <summary>5. Creating Haptic Zones (SpecialHaptics)</summary>

#### The `SpecialHaptics` script enables objects to trigger a custom haptic effect when touch.

![image](https://github.com/user-attachments/assets/15bc96c7-db42-452c-adeb-68b657984802)

To set up `SpecialHaptics`:
1. Ensure the object has a **Collider (Trigger)** attached to the same GameObject.
2. Since the interaction is physics-based, adjust the size of the collider for the haptic zone.
3. Select the type of Haptics in the inspector.

#### SpecialHaptics Settings:
- **Custom Vibrations:**  
  - When activated will create the vibration effects.
  - *Frequency Speed:* the frequency of the vibrations.
  - *Haptic Strength:* the strength of the vibrations.
- **Custom Haptics:**
  - When activated/touch will trigger a constant haptic.
  - *Haptic Pressure:* slider to adjust strength of haptic. 10 = weakest, 60 = strongest.
- **Fountain Effect:**  
  - When activated will simulate running water.
 
- **Raindrop Effect:**  
  - When activated will simulate raindrops with random haptics trigger.
    
- **Heart Beat Effect:**  
  - When activated will simulate beating heart, but only affects fingers and not palm.
    
- **Hand Squeeze Effect:**  
  - When activated will allows the player to trigger an event by squeezing the hand
  - `0.1`: Fully closed hand  
  - `1`: Fully open hand
</details> 

<details>
  <summary>6. Determine if hand is near (ProximityCheck)</summary>

#### The `ProximityCheck` script checks if the left or right hand is near the target object.
#### Haptics is only trigger when the hand is near the object.
#### Place the `ProximityCheck` prefab as a child of the target object and click the auto set up.
#### You should adjust the size of your trigger collider to ensure that it is optimise.


</details> 
</details>

&nbsp;


<details>
<summary> Demo Scene : Basic Tutorial </summary>
 
## **Demo Scene : Basic Tutorial **

#### The **Basic Tutorial ** demo scene contains the implementation to grab and pinch object using HexR grabbing and pinching.

![image](https://github.com/user-attachments/assets/a5ecd879-2c42-4e4b-a056-69a30dbceaec)

- Apple Object 🍎
  - The HexRGrabbable script is attach to the apple to allow it to be pick up. Palm grab have been selected and a haptics of 50 is triggered upon grab.
  - Gravity bool have been turned on, hence when you release the apple, it will be affected by gravity.

- Key Object 🔑
  - The HexRGrabbable script is attach to the apple to allow it to be pick up. Pinch grab have been selected and a haptics of 30 is triggered upon grab.
  - Gravity bool have been turned off.

- Torch Object 🔥
  - The HexRGrabbable script is attach to the torch to allow it to be pick up. Palm grab have been selected and a haptics of 40 is triggered upon grab.
  - Gravity bool have been turned off.
  - The SpecialHaptics is attach to the haptic zone(child gameobject) to allow vibrations to be triggered when touching the fire.

- Button Object 🎮
  - Button objects uses XR interaction and haptics is triggered from the events when the buttons is push.
  - Take a look at Open XR documentation to understand how to implement their hands interactions.
</details>

<details>
<summary> Demo Scene : Rain and Fountain Tutorial </summary>
 
## **  Demo Scene : Rain and Fountain Tutorial ⛲ **

#### The **Rain and Fountain Tutorial** demo scene contains the haptics implementations for using triggers and colliders to trigger haptics. 
#### There is a haptic zone in the fountain and rain clouds.
#### To create a haptic zone simply attach the `SpecialHaptics` Script and a collider(trigger) to a gameobject.

![image](https://github.com/user-attachments/assets/961d80fa-59ed-4431-a33e-46df43450ca8)


</details>



 
