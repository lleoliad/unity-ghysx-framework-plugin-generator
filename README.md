![](Documentation~/images/icon.png)

# GhysX Framework Plugin Generator

[![license](https://img.shields.io/github/license/lleoliad/unity-ghysx-framework?color=blue)](https://github.com/lleoliad/unity-ghysx-framework/blob/master/LICENSE)

[English](./README.md) | [Chinese](./Documentation~/README-zh.md)

## Overview
GhysX Framework Plugin Generator is a tool to extend the Unity Editor, capable of automatically generating a development environment for a Unity plugin. This tool generates the necessary file structure and configuration files based on the plugin's basic information, helping developers quickly set up a plugin development environment.

## Features
- Automatically generate the plugin's directory structure.
- Generate `package.json` file, which describes the plugin's basic information.
- Generate `CHANGELOG.md` file, which records the plugin's change history.
- Generate `LICENSE` file, containing the MIT License.
- Generate `Link.xml` file, which configures the Linker.
- Generate `README.md` file, providing usage instructions for the plugin.
- Generate `Third Party Notices.md` file, listing third-party libraries and their licenses.
- Generate `AssemblyInfo.cs` file, containing metadata for the assembly.
- Generate `asmdef` file, defining the assembly configuration.

## Installation
1. Open the Unity Editor.
2. Open the Package Manager window (Window > Package Manager).
3. Click the '+' button in the top-right corner, and select 'Add package from git URL...'.
4. Enter the following URL: `https://github.com/lleoliad/unity-ghysx-framework-plugin-generator.git`.
5. Click the 'Add' button.

## Usage
1. After installation, in the Unity Editor, open the configuration window by selecting `GhysX/Tools/Plugin Generator` from the menu bar.
2. In the configuration window, fill in the basic information of the plugin, including the plugin name, author, version, Unity version, description, keywords, email, URL, and category.
3. After filling in the information, click the 'Generate' button, and the tool will automatically generate the plugin's development environment.

## Configuration Window Description
- **Root Path**: Displays the root path where the plugin will be generated, defaulting to the `Assets` directory of the Unity project.
- **Plugin Name**: Enter the name of the plugin.
- **Author**: Enter the author of the plugin.
- **Version**: Enter the version number of the plugin.
- **Unity**: Enter the supported Unity version.
- **Description**: Enter the description of the plugin.
- **Keywords**: Enter keywords for the plugin, used for searching and categorization.
- **Email**: Enter the author's email.
- **URL**: Enter the GitHub repository address or other relevant link of the plugin.
- **Category**: Enter the category of the plugin.

## Contribution
We welcome contributions to the project! If you find any issues or have feature suggestions, please submit an Issue in the GitHub repository. If you wish to contribute code, follow these steps:
1. Fork the repository and create your branch from `main`.
2. Commit your changes and add a clear description.
3. Perform thorough testing to ensure existing tests pass.
4. Update the documentation if necessary.
5. Push your changes to your Fork and submit a Pull Request to the `main` branch.

**Tip**:
- Follow the project's code style and conventions.
- Keep the PR focused and reference related Issues.

We are very grateful for your help! ✨