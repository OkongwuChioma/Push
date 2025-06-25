# Push
 sends notifications to phones
# Firebase Cloud Messaging Service for .NET 8

A lightweight, production-ready implementation for sending push notifications to Android devices using Firebase Cloud Messaging (FCM) in a .NET 8 Web API application.

## Prerequisites

- .NET 8 SDK
- Firebase Project with FCM enabled
- Firebase Admin SDK service account key file

## Setup

1. Clone the repository
2. Place your Firebase Admin SDK service account JSON file in the `App_Data` directory
3. Update the following in `FCMService.cs`:
   - `_projectId`: Your Firebase project ID
   - `_firebaseKeyPath`: Path to your service account JSON file (if different)

## Quick Start

1. Install required packages:
