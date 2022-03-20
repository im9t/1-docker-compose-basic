FROM ubuntu
WORKDIR /myGocd

COPY go-agent.deb ./
ENV DEBIAN_FRONTEND noninteractive
ENV DEBCONF_NONINTERACTIVE_SEEN true
RUN dpkg -i go-agent.deb && sed -iE  \
's/=http.*go/=http\:\/\/www\.elight\.fun\:2022\/go/g' \
 /usr/share/go-agent/wrapper-config/wrapper-properties.conf

 RUN apt-get update && apt-get install -y\
    ca-certificates \
    curl \
    gnupg \
    lsb-release\
    git &&  curl -fsSL https://download.docker.com/linux/ubuntu/gpg |  gpg --dearmor -o /usr/share/keyrings/docker-archive-keyring.gpg && \
    echo \
  "deb [arch=$(dpkg --print-architecture) signed-by=/usr/share/keyrings/docker-archive-keyring.gpg] https://download.docker.com/linux/ubuntu \
  $(lsb_release -cs) stable" | tee /etc/apt/sources.list.d/docker.list > /dev/null && \
  apt-get update && \
  apt-get install -y docker-ce docker-ce-cli containerd.io \
 && usermod -aG root go && usermod -aG ssh go

CMD "service" "go-agent" "console"
#CMD [ "/usr/share/go-agent console" ]