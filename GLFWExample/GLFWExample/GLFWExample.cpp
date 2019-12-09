
#include <iostream>
#include "main.h"

int main(int argc, char **argv)
{
	// GLFWwindow Test
	GLFWwindow* window;

	glewExperimental = true;

	if (!glfwInit())
		return -1;

	glfwWindowHint(GLFW_SAMPLES, 4); // 4x antialiasing
	glfwWindowHint(GLFW_CONTEXT_VERSION_MAJOR, 3); // Set (원하는) Opengl Version 
	glfwWindowHint(GLFW_CONTEXT_VERSION_MINOR, 3);
	glfwWindowHint(GLFW_OPENGL_FORWARD_COMPAT, GL_TRUE);
	glfwWindowHint(GLFW_OPENGL_PROFILE, GLFW_OPENGL_CORE_PROFILE);

	window = glfwCreateWindow(640, 480, "Main Window", NULL, NULL);
	if (!window)
	{
		glfwTerminate();
		return -1;
	}

	glfwMakeContextCurrent(window); // Setting drawing paper
	glewInit(); // Initiate glew
	glClearColor(0.5, 0.5, 0, 0.3); // Init BackGround Color

	glfwSetInputMode(window, GLFW_STICKY_KEYS, GL_FALSE);
}