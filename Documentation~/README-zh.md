![](Documentation~/images/icon.png)

# GhysX Framework Plugin Generator

[![license](https://img.shields.io/github/license/lleoliad/unity-ghysx-framework?color=blue)](https://github.com/lleoliad/unity-ghysx-framework/blob/master/LICENSE)
[![release](https://img.shields.io/github/v/tag/lleoliad/unity-ghysx-framework-plugin-generator?label=release)](https://github.com/lleoliad/unity-ghysx-framework-plugin-generator/releases)
[![openupm](https://img.shields.io/npm/v/com.lleoliad.ghysx-framework-plugin-generator?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.lleoliad.ghysx-framework-plugin-generator/)

[English](../README.md) | [中文](./Documentation~/README-zh.md)

## 概述
GhysX Framework Plugin Generator 是一个用于扩展 Unity 编辑器的工具，能够自动生成一个 Unity 插件的开发环境。该工具通过配置插件的基本信息，自动生成必要的文件结构和配置文件，帮助开发者快速搭建插件开发环境。

## 功能
- 自动生成插件的目录结构。
- 生成 `package.json` 文件，用于描述插件的基本信息。
- 生成 `CHANGELOG.md` 文件，记录插件的变更历史。
- 生成 `LICENSE` 文件，包含 MIT 许可证。
- 生成 `Link.xml` 文件，用于配置 Linker。
- 生成 `README.md` 文件，提供插件的使用说明。
- 生成 `Third Party Notices.md` 文件，列出第三方库及其许可证。
- 生成 `AssemblyInfo.cs` 文件，包含程序集的元数据。
- 生成 `asmdef` 文件，定义程序集的配置。

## 安装

### 通过git URL安装
1. 打开 Unity 编辑器。
2. 打开 Package Manager 窗口（Window > Package Manager）。
3. 点击右上角的 '+' 按钮，选择 'Add package from git URL...'。
4. 输入以下 URL: `https://github.com/lleoliad/unity-ghysx-framework-plugin-generator.git`。
5. 点击 'Add' 按钮。

### 使用 OpenUPM 安装

[OpenUPM](https://openupm.com/) 是一个开源的UPM包仓库，它支持发布第三方的UPM包，它能够自动管理包的依赖关系，可以使用它安装本框架.

通过openupm命令安装包,要求[nodejs](https://nodejs.org/en/download/) and openupm-cli客户端的支持，如果没有安装请先安装nodejs和open-cli。

    # 使用npm命令安装openupm-cli，如果已经安装请忽略.
    npm install -g openupm-cli 
    
    #切换当前目录到项目的根目录
    cd YOUR_UNITY_PROJECT_FOLDER
    
    #Install ghysx-framework-plugin-generator
    openupm add com.lleoliad.ghysx-framework-plugin-generator

## 使用
1. 安装完成后，在 Unity 编辑器中，通过菜单栏选择 `GhysX/Tools/Plugin Generator` 打开配置窗口。
2. 在配置窗口中填写插件的基本信息，包括插件名称、作者、版本、Unity 版本、描述、关键词、电子邮件、URL 和类别。
3. 填写完成后，点击 'Generate' 按钮，工具将自动生成插件的开发环境。

## 配置窗口说明
- **Root Path**: 显示插件生成的根路径，默认为 Unity 项目的 `Assets` 目录。
- **Plugin Name**: 输入插件的名称。
- **Author**: 输入插件的作者。
- **Version**: 输入插件的版本号。
- **Unity**: 输入支持的 Unity 版本。
- **Description**: 输入插件的描述信息。
- **Keywords**: 输入插件的关键词，用于搜索和分类。
- **Email**: 输入作者的电子邮件。
- **URL**: 输入插件的 GitHub 仓库地址或其他相关链接。
- **Category**: 输入插件的类别。

## 贡献
欢迎对项目进行贡献！如果您发现任何问题或有功能建议，请在 GitHub 仓库中提交 Issue。如果您希望贡献代码，请按照以下步骤操作：
1. Fork 仓库并从 `main` 分支创建您的分支。
2. 提交更改并添加清晰的描述。
3. 进行充分的测试，确保现有测试通过。
4. 如果需要，更新文档。
5. 将更改推送到您的 Fork 并提交 Pull Request 到 `main` 分支。

**提示**:
- 遵循项目的代码风格和约定。
- 保持 PR 焦点，并引用相关 Issue。

我们非常感谢您的帮助！✨