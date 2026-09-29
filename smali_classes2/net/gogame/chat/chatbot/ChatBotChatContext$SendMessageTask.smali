.class Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask;
.super Landroid/os/AsyncTask;
.source "ChatBotChatContext.java"


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/chat/chatbot/ChatBotChatContext;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x2
    name = "SendMessageTask"
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Landroid/os/AsyncTask<",
        "Ljava/lang/String;",
        "Ljava/lang/Void;",
        "Ljava/lang/Void;",
        ">;"
    }
.end annotation


# instance fields
.field final synthetic this$0:Lnet/gogame/chat/chatbot/ChatBotChatContext;


# direct methods
.method private constructor <init>(Lnet/gogame/chat/chatbot/ChatBotChatContext;)V
    .locals 0

    .line 217
    iput-object p1, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask;->this$0:Lnet/gogame/chat/chatbot/ChatBotChatContext;

    invoke-direct {p0}, Landroid/os/AsyncTask;-><init>()V

    return-void
.end method

.method synthetic constructor <init>(Lnet/gogame/chat/chatbot/ChatBotChatContext;Lnet/gogame/chat/chatbot/ChatBotChatContext$1;)V
    .locals 0

    .line 217
    invoke-direct {p0, p1}, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask;-><init>(Lnet/gogame/chat/chatbot/ChatBotChatContext;)V

    return-void
.end method

.method private send(Ljava/lang/String;)V
    .locals 13

    .line 229
    :try_start_0
    new-instance v1, Ljava/net/URL;

    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "https://gw-chat.gogame.net/webchat/receive/"

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    iget-object v3, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask;->this$0:Lnet/gogame/chat/chatbot/ChatBotChatContext;

    invoke-static {v3}, Lnet/gogame/chat/chatbot/ChatBotChatContext;->access$100(Lnet/gogame/chat/chatbot/ChatBotChatContext;)Lnet/gogame/chat/chatbot/ChatBotConfig;

    move-result-object v3

    invoke-virtual {v3}, Lnet/gogame/chat/chatbot/ChatBotConfig;->getAppId()Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v2

    invoke-direct {v1, v2}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    .line 230
    invoke-virtual {v1}, Ljava/net/URL;->openConnection()Ljava/net/URLConnection;

    move-result-object v1

    move-object v9, v1

    check-cast v9, Ljava/net/HttpURLConnection;
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    const/4 v1, 0x1

    .line 232
    :try_start_1
    invoke-virtual {v9, v1}, Ljava/net/HttpURLConnection;->setDoOutput(Z)V

    const/4 v1, 0x0

    .line 233
    invoke-virtual {v9, v1}, Ljava/net/HttpURLConnection;->setChunkedStreamingMode(I)V

    const-string v2, "Content-Type"

    const-string v3, "application/json"

    .line 234
    invoke-virtual {v9, v2, v3}, Ljava/net/HttpURLConnection;->setRequestProperty(Ljava/lang/String;Ljava/lang/String;)V

    .line 236
    new-instance v10, Ljava/io/BufferedOutputStream;

    .line 237
    invoke-virtual {v9}, Ljava/net/HttpURLConnection;->getOutputStream()Ljava/io/OutputStream;

    move-result-object v2

    invoke-direct {v10, v2}, Ljava/io/BufferedOutputStream;-><init>(Ljava/io/OutputStream;)V
    :try_end_1
    .catchall {:try_start_1 .. :try_end_1} :catchall_2

    .line 239
    :try_start_2
    new-instance v2, Lorg/json/JSONObject;

    invoke-direct {v2}, Lorg/json/JSONObject;-><init>()V

    const-string v3, "user_id"

    .line 240
    iget-object v4, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask;->this$0:Lnet/gogame/chat/chatbot/ChatBotChatContext;

    invoke-static {v4}, Lnet/gogame/chat/chatbot/ChatBotChatContext;->access$200(Lnet/gogame/chat/chatbot/ChatBotChatContext;)Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v2, v3, v4}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    if-eqz p1, :cond_0

    const-string v3, "message"

    .line 242
    invoke-virtual {v2, v3, p1}, Lorg/json/JSONObject;->put(Ljava/lang/String;Ljava/lang/Object;)Lorg/json/JSONObject;

    .line 244
    :cond_0
    invoke-virtual {v2}, Lorg/json/JSONObject;->toString()Ljava/lang/String;

    move-result-object v0

    const-string v2, "UTF-8"

    .line 245
    invoke-virtual {v0, v2}, Ljava/lang/String;->getBytes(Ljava/lang/String;)[B

    move-result-object v0

    invoke-virtual {v10, v0}, Ljava/io/OutputStream;->write([B)V

    .line 246
    invoke-virtual {v10}, Ljava/io/OutputStream;->flush()V

    .line 247
    invoke-virtual {v10}, Ljava/io/OutputStream;->close()V

    .line 252
    new-instance v11, Ljava/io/BufferedInputStream;

    .line 253
    invoke-virtual {v9}, Ljava/net/HttpURLConnection;->getInputStream()Ljava/io/InputStream;

    move-result-object v0

    invoke-direct {v11, v0}, Ljava/io/BufferedInputStream;-><init>(Ljava/io/InputStream;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_1

    .line 255
    :try_start_3
    new-instance v0, Ljava/io/ByteArrayOutputStream;

    invoke-direct {v0}, Ljava/io/ByteArrayOutputStream;-><init>()V

    .line 256
    invoke-static {v11, v0}, Lnet/gogame/chat/IOUtils;->copy(Ljava/io/InputStream;Ljava/io/OutputStream;)V

    .line 257
    new-instance v2, Ljava/lang/String;

    invoke-virtual {v0}, Ljava/io/ByteArrayOutputStream;->toByteArray()[B

    move-result-object v0

    const-string v3, "UTF-8"

    invoke-direct {v2, v0, v3}, Ljava/lang/String;-><init>([BLjava/lang/String;)V

    .line 261
    new-instance v0, Lorg/json/JSONObject;

    invoke-direct {v0, v2}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V

    const-string v2, "timestamp"

    .line 264
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v3

    .line 263
    invoke-virtual {v0, v2, v3, v4}, Lorg/json/JSONObject;->optLong(Ljava/lang/String;J)J

    move-result-wide v4

    const-string v2, "agentId"

    const-string v3, "default"

    .line 265
    invoke-virtual {v0, v2, v3}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    const-string v2, "agentDisplayName"

    const-string v3, "Sarah"

    .line 266
    invoke-virtual {v0, v2, v3}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    const-string v2, "agentAvatarUri"

    .line 269
    invoke-static {}, Lnet/gogame/chat/chatbot/ChatBotChatContext;->access$300()Ljava/lang/String;

    move-result-object v3

    .line 268
    invoke-virtual {v0, v2, v3}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v8

    const-string v2, "response"

    .line 270
    invoke-virtual {v0, v2}, Lorg/json/JSONObject;->optJSONArray(Ljava/lang/String;)Lorg/json/JSONArray;

    move-result-object v0

    .line 271
    new-instance v3, Ljava/util/ArrayList;

    invoke-direct {v3}, Ljava/util/ArrayList;-><init>()V

    if-eqz v0, :cond_2

    .line 273
    :goto_0
    invoke-virtual {v0}, Lorg/json/JSONArray;->length()I

    move-result v2

    if-ge v1, v2, :cond_2

    const/4 v2, 0x0

    .line 275
    invoke-virtual {v0, v1, v2}, Lorg/json/JSONArray;->optString(ILjava/lang/String;)Ljava/lang/String;

    move-result-object v2

    if-eqz v2, :cond_1

    .line 277
    invoke-interface {v3, v2}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :cond_1
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    .line 281
    :cond_2
    invoke-interface {v3}, Ljava/util/List;->isEmpty()Z

    move-result v0

    if-nez v0, :cond_3

    .line 282
    iget-object v0, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask;->this$0:Lnet/gogame/chat/chatbot/ChatBotChatContext;

    invoke-static {v0}, Lnet/gogame/chat/chatbot/ChatBotChatContext;->access$500(Lnet/gogame/chat/chatbot/ChatBotChatContext;)Landroid/app/Activity;

    move-result-object v0

    new-instance v12, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask$1;

    move-object v1, v12

    move-object v2, p0

    invoke-direct/range {v1 .. v8}, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask$1;-><init>(Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask;Ljava/util/List;JLjava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    invoke-virtual {v0, v12}, Landroid/app/Activity;->runOnUiThread(Ljava/lang/Runnable;)V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_0

    .line 301
    :cond_3
    :try_start_4
    invoke-static {v11}, Lnet/gogame/chat/IOUtils;->closeQuietly(Ljava/io/InputStream;)V
    :try_end_4
    .catchall {:try_start_4 .. :try_end_4} :catchall_1

    .line 304
    :try_start_5
    invoke-static {v10}, Lnet/gogame/chat/IOUtils;->closeQuietly(Ljava/io/OutputStream;)V
    :try_end_5
    .catchall {:try_start_5 .. :try_end_5} :catchall_2

    .line 307
    :try_start_6
    invoke-virtual {v9}, Ljava/net/HttpURLConnection;->disconnect()V
    :try_end_6
    .catch Ljava/lang/Exception; {:try_start_6 .. :try_end_6} :catch_0

    goto :goto_1

    :catchall_0
    move-exception v0

    .line 301
    :try_start_7
    invoke-static {v11}, Lnet/gogame/chat/IOUtils;->closeQuietly(Ljava/io/InputStream;)V

    throw v0
    :try_end_7
    .catchall {:try_start_7 .. :try_end_7} :catchall_1

    :catchall_1
    move-exception v0

    .line 304
    :try_start_8
    invoke-static {v10}, Lnet/gogame/chat/IOUtils;->closeQuietly(Ljava/io/OutputStream;)V

    throw v0
    :try_end_8
    .catchall {:try_start_8 .. :try_end_8} :catchall_2

    :catchall_2
    move-exception v0

    .line 307
    :try_start_9
    invoke-virtual {v9}, Ljava/net/HttpURLConnection;->disconnect()V

    throw v0
    :try_end_9
    .catch Ljava/lang/Exception; {:try_start_9 .. :try_end_9} :catch_0

    :catch_0
    move-exception v0

    const-string v1, "zopim-client"

    const-string v2, "Exception"

    .line 310
    invoke-static {v1, v2, v0}, Landroid/util/Log;->e(Ljava/lang/String;Ljava/lang/String;Ljava/lang/Throwable;)I

    .line 311
    new-instance v0, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;

    invoke-direct {v0}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;-><init>()V

    .line 312
    sget-object v1, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;->CHAT_MSG_SYSTEM:Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;

    invoke-virtual {v0, v1}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->setType(Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog$Type;)V

    .line 313
    invoke-static {}, Landroid/os/SystemClock;->currentThreadTimeMillis()J

    move-result-wide v1

    invoke-virtual {v0, v1, v2}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->setTimestamp(J)V

    const-string v1, "System currently unavailable, we apologize for the inconvenience."

    .line 314
    invoke-virtual {v0, v1}, Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;->setMessage(Ljava/lang/String;)V

    .line 316
    iget-object v1, p0, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask;->this$0:Lnet/gogame/chat/chatbot/ChatBotChatContext;

    invoke-static {v1, v0}, Lnet/gogame/chat/chatbot/ChatBotChatContext;->access$600(Lnet/gogame/chat/chatbot/ChatBotChatContext;Lnet/gogame/chat/chatbot/ChatBotChatContext$ChatLog;)V

    :goto_1
    return-void
.end method


# virtual methods
.method protected bridge synthetic doInBackground([Ljava/lang/Object;)Ljava/lang/Object;
    .locals 0

    .line 217
    check-cast p1, [Ljava/lang/String;

    invoke-virtual {p0, p1}, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask;->doInBackground([Ljava/lang/String;)Ljava/lang/Void;

    move-result-object p1

    return-object p1
.end method

.method protected varargs doInBackground([Ljava/lang/String;)Ljava/lang/Void;
    .locals 3

    .line 221
    array-length v0, p1

    const/4 v1, 0x0

    :goto_0
    if-ge v1, v0, :cond_0

    aget-object v2, p1, v1

    .line 222
    invoke-direct {p0, v2}, Lnet/gogame/chat/chatbot/ChatBotChatContext$SendMessageTask;->send(Ljava/lang/String;)V

    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_0
    const/4 p1, 0x0

    return-object p1
.end method
