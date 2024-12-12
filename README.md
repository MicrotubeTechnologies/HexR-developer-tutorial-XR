# HexR Unity Integration (Uses Meta OVR) ℹ️

## Installation

#### Make sure you're using Unity 2021.3.26f1 or newer.
#### For projects using Meta OVR refer to : [https://github.com/MicrotubeTechnologies/HaptGlove_Example](https://github.com/MicrotubeTechnologies/HexR-Developer-Tutorial-Meta-OVR)
#### Clone this repo 
[https://github.com/MicrotubeTechnologies/HexR-Developer-Tutorial.git](https://github.com/MicrotubeTechnologies/HexR-developer-tutorial-XR/tree/main)
#### Then, open the HexR Developer Tutorial project in Unity.



<details>
<summary> [  HexR Essential Scripts ] </summary>
 
 #### Learn more about the framework for HexR through this essential scripts 💡
 
 <details> 
 <summary>  1. Hand Tracking ( PhysicsHandTracking ) </summary>

 #### HexR hand supports both the Open XR and Meta OVR hand skeleton structure.
 #### The difference in the hand structure is summarise in the illustration.
 #### The Script PhysicsHandTracking is responsible to mimic the OVR/XR hands
![Hand Skeleton](https://github.com/user-attachments/assets/2585a044-ae44-4814-88e5-abe61c876f8e)
 </details>


 <details>  
 <summary>  2. HexR Overall Manager ( HaptGloveManager ) </summary>
  
 #### The HaptGloveManager allow users to automate the set up process.
 #### In the inspector, you are able to select the XR framework and click on the "Auto Set Up HexR" button.
 #### Check the debug log to ensure set up is successful.

![image](https://github.com/user-attachments/assets/f09f713f-fa81-484e-8646-bbe830ecce35)
</details>

 <details>  
 <summary>  3. Haptics Controller ( PressureTrackerMain ) </summary>
  
 #### The PressureTrackerMain contains the functions to call the haptics related functions.
 #### Functions are categorise by single channel triggers or multiple channel triggers, take a look at the demo to see how they are used.
 
</details>

&nbsp;
</details>

<details>
<summary> [  Demo Scene : Basic Tutorial ] </summary>
 
## **Demo Scene : Basic Tutorial **

#### The **Basic Tutorial ** demo scene contains the implementation to grab and pinch object using HexR grabbing and pinching.
</details>

<details>
<summary> [  Demo Scene : Rain and Fountain Tutorial ] </summary>
 
## **Demo Scene : Rain and Fountain Tutorial**

#### The **Rain and Fountain Tutorial** demo scene contains the haptics implementations for using triggers and colliders to trigger haptics. 
</details>



 
