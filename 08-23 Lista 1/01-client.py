import socket

nome = input("Digite o nome: ")
cargo = input("Digite o cargo: ")
salario = float(input("Digite o salário: "))

mensagem = nome + "\n" + cargo + "\n" + str(salario)

#============================

client = socket.socket()

client.connect(("127.0.0.1", 8888))

buf = mensagem.encode("utf8")

client.send(len(buf).to_bytes(4, "little"))
client.send(buf)

#============================

buf = bytearray()

while len(buf) < 4:
    buf += client.recv(4)

message_len = int.from_bytes(buf[0:4], "little")

buf = buf[4:]

while len(buf) < message_len:
    buf += client.recv(message_len - len(buf))

mensagem = buf.decode("utf8")

#============================

print()
print("Nome: " + nome)
print("Novo Salário: " + mensagem)

client.close()