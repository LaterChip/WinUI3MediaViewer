# WinUI3 Media Viewer

一个基于 WinUI 3 的轻量级媒体查看器，支持拖放浏览图片（jpg/png/ico）和音视频（mp3/mp4）。

![截图](screenshot.png) *(请自行添加截图)*

## 特性

- 📁 拖放支持：将文件或文件夹拖入窗口即可自动加载所有支持的媒体文件。
- 🖼️ 图片预览：支持 JPG、PNG、ICO 等常见图片格式，自动生成缩略图。
- 🎵 音频播放：支持 MP3 格式，内置播放控制（播放/暂停/进度条）。
- 🎬 视频播放：支持 MP4 格式，使用系统 MediaPlayer 播放。
- 🗂️ 文件列表：左侧以缩略图列表形式展示所有已加载文件。
- ⚡ 快速切换：单击列表项即可切换预览内容。
- 🧹 清空列表：一键清空当前所有文件。

## 系统要求

- Windows 10 版本 1809（内部版本 17763）或更高版本
- 安装 [Windows App SDK 运行时](https://learn.microsoft.com/zh-cn/windows/apps/windows-app-sdk/downloads)

## 开发环境

- Visual Studio 2022（17.0 或更高）
- .NET 6.0 SDK
- Windows App SDK 1.5

## 如何编译与运行

1. 克隆本仓库：
   ```bash
   git clone https://github.com/yourusername/WinUI3MediaViewer.git
