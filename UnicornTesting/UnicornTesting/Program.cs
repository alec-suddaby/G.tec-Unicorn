// To install:
// Import nuget package
// Download repo from GitHub
// Build with CMake (tools/build.py)
// CMake can be installed with apt-get
// Run program with sudo dotnet run from directory as it won't find the headset without sudo
// A driver was also downloaded from the G.tec repository. Though, I'm not sure it was neccessary. The so file from there was moved to the general lib folder

using brainflow;
using brainflow.math;

Console.WriteLine("Begun");
int boardId = (int)BoardIds.UNICORN_BOARD;

BoardShim.enable_dev_board_logger();

//No special input parameters required for G.tec Unicorn https://brainflow.readthedocs.io/en/stable/SupportedBoards.html#unicorn
BrainFlowInputParams inputParams = new BrainFlowInputParams();
BoardShim boardShim = new BoardShim(boardId, inputParams);

Console.WriteLine("Read Start\n\n");
boardShim.prepare_session();
boardShim.start_stream ();
Thread.Sleep (5000);
boardShim.stop_stream ();
double[,] unprocessed_data = boardShim.get_current_board_data (20);
int[] eeg_channels = BoardShim.get_eeg_channels (boardId);
foreach (var index in eeg_channels)
    Console.WriteLine ("[{0}]", string.Join (", ", unprocessed_data.GetRow (index)));
boardShim.release_session ();

Console.WriteLine("\n\nRead Complete");