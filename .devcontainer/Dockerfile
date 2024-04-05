FROM mcr.microsoft.com/devcontainers/dotnet:1-8.0-bookworm

# Install SQL Tools: SQLPackage and sqlcmd
COPY ./.devcontainer/mssql/installSQLtools.sh installSQLtools.sh
RUN bash ./installSQLtools.sh \
     && apt-get clean -y && rm -rf /var/lib/apt/lists/* /tmp/library-scripts

# Install oh-my-posh
RUN wget https://github.com/JanDeDobbeleer/oh-my-posh/releases/latest/download/posh-linux-amd64 -O /usr/local/bin/oh-my-posh
RUN chmod +x /usr/local/bin/oh-my-posh

# Download and install the font
RUN wget https://github.com/ryanoasis/nerd-fonts/releases/download/v2.1.0/FiraCode.zip
RUN apt-get install -y unzip && unzip FiraCode.zip -d /usr/share/fonts

# Configure bash to use oh-my-posh
RUN echo "eval \"$(oh-my-posh --init --shell bash --config ohmyposhconfig.json)\"" >> /home/vscode/.bashrc

