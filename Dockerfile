FROM mono:5.12
RUN mkdir -p /sharpchat
WORKDIR /sharpchat
COPY . /sharpchat
RUN apt update && apt install -y gtk3.0
CMD ["mono", "./SharpClient/bin/Debug/SharpClient.exe" ]
