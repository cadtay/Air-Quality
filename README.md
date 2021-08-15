# Air-Quality Frontend

Using Node version 10.10.0

Open up a new terminal and navigate to the air-quality directory and the run 'npm install'.

Then run 'npm run serve'

## Note

If using a private NPM registry there might be some packages missing in your registry.

In that case you can create a separate NPMRC profile by running:

npmrc -c <name-of-profile>

npm config set registry https://registry.npmjs.org/

then switch profile by running npmrc <profile-name>

then re-run 'npm install' then NPM will now point to the default registry to get packages.

## Project setup
```
npm install
```

### Compiles and hot-reloads for development
```
npm run serve
```

### Compiles and minifies for production
```
npm run build
```

### Lints and fixes files
```
npm run lint
```

### Customize configuration
See [Configuration Reference](https://cli.vuejs.org/config/).
