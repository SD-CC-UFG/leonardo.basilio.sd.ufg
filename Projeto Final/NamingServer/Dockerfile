FROM golang:latest as build-env

# All these steps will be cached
RUN mkdir /app
WORKDIR /app
# <- COPY go.mod and go.sum files to the workspace
COPY go.mod . 
COPY go.sum .

# Get dependancies - will also be cached if we won't change mod/sum
RUN go mod download
# COPY the source code as the last step
COPY . .

# Build the binary
RUN CGO_ENABLED=0 GOOS=linux GOARCH=amd64 go build -a -installsuffix cgo -o /go/bin/app

# <- Second step to build minimal image
FROM scratch 
COPY --from=build-env /go/bin/app /go/bin/app
COPY --from=build-env /app/services.conf .

EXPOSE 7777/tcp
ENTRYPOINT ["/go/bin/app"]
