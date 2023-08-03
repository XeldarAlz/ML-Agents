# Unity ML Agents Repository

This is a repository for Unity Machine Learning Agents, an open-source Unity plugin that enables games and simulations to serve as environments for training intelligent agents.

## Requirements
1. Unity (2021.3 or later)
2. Python (3.8.13 or higher)
3. `com.unity.ml-agents` Unity package
4. `mlagents` Python package

## Setup
### Step 1: Install Unity and Python
You will need to have Unity (version 2021.3 or later) and Python (version 3.8.13 or higher) installed on your machine.

### Step 2: Set up Virtual Environment
Navigate to your preferred directory (not the repository directory) on your PC, then run the following command to create a virtual environment. Change the last venv for your desire since it is folder name:
`python -m venv venv`

### Step 3: Activate the Virtual Environment
Activate the virtual environment to start working with the ML Agents:
`venv\Scripts\activate`

### Step 4: Install Python Package Manager
Upgrade pip, the Python package manager:
`python -m pip install --upgrade pip`

### Step 5: Install ML Agents
Now, install the ml-agents package:
`pip install mlagents`

### Step 6: Install PyTorch
PyTorch, a deep learning framework, is required for working with ML Agents. Install PyTorch along with torchvision and torchaudio:
`pip3 install torch torchvision torchaudio`

### Step 7: Install Protocol Buffers
Install the specific version (3.20.3) of Protocol Buffers because ml agents package installs the wrong version for some reason:
`pip install protobuf==3.20.3`

### Step 8: Install Onnx Package (Situational)
When you finish your training, if you get a torch error on your terminal like `Module onnx is not installed!`, Run the following command:
`pip install onnx`

### Step 9: Finish
After all these installations, if everything is correctly installed, you should be able to see all the commands when you run:
`mlagents-learn --help`

## Training
Before training the ML agents, make sure that the virtual environment is activated. If it is not activated , repeat `Step 3`. Then use the following command to initiate the training. Make sure to replace "Test1" with a unique identifier each time:`mlagents-learn --run-id=Test1`. When you see the unity logo on your terminal, press play on the Unity scene to begin training the agents. Check their progress on the terminal window with each step. Training will end when steps reaches `max_steps` value in your config file. If you want to manually end the training, press `CTRL+C` on your terminal to terminate the training process. Enjoy teaching your agents and making them work like ants!

## Patience
If you want to improve the learning speed of your Unity ML agents, here are some suggestions:
1. Hyperparameters can have a huge impact on the learning speed of your agents. Learning rate, batch size, number of layers in your neural network, number of hidden units per layer, etc., can be adjusted to improve learning speed. However, note that these adjustments may require a lot of trial and error. So experiment with your config file to find the best one.
2. Always start with the simplest task For example, if your agent is learning to play a game, you could start with simpler scenarios and then introduce more challenging ones as the agent improves.
3. Use multiple instances. By using multiple parallel instances, you can increase the number of experiences your agent gets in the same amount of time, leading to faster learning. ML Agents allows running multiple environments in parallel. Check my scene for an example.
4. Crafting a good reward function is crucial for training speed. Rewards should be frequent enough to provide useful learning signals and sparse enough to push the agent to explore. Additionally, consider normalizing your rewards to a consistent scale, like -1 to 1, to prevent the agent from prioritizing certain types of rewards over others.
5. If possible, reduce the dimensionality of the state space your agent has to learn from. For example, if your agent gets a 100-dimensional vector of information but only 10 of those dimensions are actually useful, consider modifying the environment so it only returns those 10 dimensions. This simplifies the task for your agent and can improve learning speed.
6. If you have a similar task that's already been solved, you could use a model pretrained on that task as a starting point for training your agent. This technique, called transfer learning, can drastically speed up training times.

## Notes
1. You can create a folder to keep your config files in your venv folder. I shared my config files in this repository so you can get them and modify for your needs.
2. If you want to run a specific config file for the training, run this command. Remember to change the directory with yours: `mlagents-learn C:\ml-agent\venv\Configs\MazeOneConfig.yaml --run-id=MazeOneRun`
3. Your training results will be on the directory of this project, but can't be seen on the unity project window like: `C:\ml-agent\src\ML-Agents\results`
4. If you want to use the model of a specific training, you need to move the `.onnx` to the assets folder. Example: Move `C:\ml-agent\src\ML-Agents\results\Test1\Test1.onnx` to `C:\ml-agent\src\ML-Agents\Assets`. So now it can be seen on the unity project window and you can use this model.
