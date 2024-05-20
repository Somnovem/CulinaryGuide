# pull official base image
FROM node:18-alpine

# set working directory
WORKDIR /app

# add `/app/node_modules/.bin` to $PATH
ENV PATH /app/node_modules/.bin:$PATH

# install app dependencies
COPY ./culinaryguide.client/package.json ./
COPY ./culinaryguide.client/package-lock.json ./
RUN npm install
RUN npm install react-scripts@3.4.1 -g 

# add app
COPY . ./

# start app
CMD ["npm", "run", "dev"]
