# PM2

PM2 is a popular process manager for Node.js applications. It helps developers manage and keep their Node.js applications running smoothly in production environments.

## What does PM2 do?

- Process Management: Keeps your Node.js apps alive forever, automatically restarting them if they crash.
- Load Balancing: Can run multiple instances of your app to utilize multi-core systems efficiently./ can manage, run multiple processes at the same time
- Monitoring: Provides real-time logs and stats about your running apps (CPU, memory usage).
- Startup Scripts: Can generate scripts to start your app automatically when the server boots.
- Easy Deployment: Integrates with deployment workflows to simplify pushing updates.

## Why use PM2?

- It simplifies running and managing Node.js apps in production.
- Avoids downtime by auto-restarting crashed apps.
- Handles clustering for better performance.
- Gives detailed insights and control over your running processes.

```JS
//After running : for example these 2 files

npx pm2 start server.js
npx pm2 start index.js

//it will start two separate processes under PM2 management — one running server.js and another running index.js.
//PM2 will treat each as an independent app/process, assign them unique IDs, and you can manage them separately (restart, stop, monitor, etc.).

//Then -->
npx pm2 list  //<-- You’ll see both processes listed with their own IDs, statuses, CPU/memory usage, and more.
```

# Both commands will run your app or file

```JS
node index.js //<-- put in terminal to run file

pm2 start index.js //<-- put in terminal to run file
```

# What command - "node index.js" does ?

- Starts your app directly with Node.js.
- If your app crashes or stops, it will not restart automatically.
- You have to manually handle process restarts or monitoring.
- No built-in load balancing or clustering.

# What "pm2 index.js" or more commonly "pm2 start index.js" does ?

- Starts your app using PM2’s process manager.
- If the app crashes, PM2 will automatically restart it.
- You get monitoring, logging, and process management features.
- You can easily run multiple instances to use multiple CPU cores.
- Provides commands to manage the app lifecycle (pm2 restart, pm2 stop, pm2 logs)

# Example of PM2 usage

```JS
//Install pm2
npm install pm2 or npx pm2

//Start an app
pm2 start app.js

//List running processes
pm2 list

//Monitor logs and resource usage
pm2 monit

//Restart app
pm2 restart app.js

//Stop app
pm2 stop app.js
```

```JS
//PM2 commands

- npx pm2 start server.js // <-- It downloads PM2 temporarily and starts your Node.js server (server.js) as a managed process
- npx pm2 stop 0 //<-- Stops the PM2-managed process with the ID 0. The ID corresponds to the process number shown in pm2 list
- npx pm2 monit 0 //<-- Opens a real-time monitoring dashboard for the process with ID 0. Shows CPU, memory, event loop delay, and other performance metrics
- npx pm2 list //<-- Displays a table of all running PM2 processes with their IDs, names, status, CPU and memory usage
- npx pm2 logs //<-- Shows live logs for all managed processes, helping you debug or monitor output
- npx pm2 restart <id or name>
- npx pm2 delete <id or name>
- npx pm2 reload <id or name>
- npx pm2 stop all  //<-- Stop all running PM2 processes
-npx pm2 restart all //<-- Restart all running processes
-npx pm2 delete all //<-- Stop and remove all processes
-npx pm2 flush //<-- Clear all logs
-npx pm2 logs <id or name>
-npx pm2 save //<-- Save current process list to be resurrected on reboot
```
