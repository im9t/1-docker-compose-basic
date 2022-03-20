FROM ubuntu

RUN echo "deb https://download.gocd.org /" | tee /etc/apt/sources.list.d/gocd.list && \
curl https://download.gocd.org/GOCD-GPG-KEY.asc | apt-key add - \ apt-get update -y && \
    apt-get install go-agent
