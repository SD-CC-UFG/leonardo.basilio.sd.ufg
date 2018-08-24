import socket

sexo = input("Digite o sexo (M ou F): ")
altura = float(input("Digite a altura (m): "))

mensagem = sexo + "\n" + str(altura)

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
print("Peso Ideal (kg): " + mensagem)

client.close()