.class public Lcom/google/firebase/messaging/cpp/MessageWriter;
.super Ljava/lang/Object;
.source "MessageWriter.java"


# static fields
.field private static final DEFAULT_INSTANCE:Lcom/google/firebase/messaging/cpp/MessageWriter;

.field static final LOCK_FILE:Ljava/lang/String; = "FIREBASE_CLOUD_MESSAGING_LOCKFILE"

.field static final STORAGE_FILE:Ljava/lang/String; = "FIREBASE_CLOUD_MESSAGING_LOCAL_STORAGE"

.field private static final TAG:Ljava/lang/String; = "FIREBASE_MESSAGE_WRITER"


# direct methods
.method static constructor <clinit>()V
    .locals 1

    .line 40
    new-instance v0, Lcom/google/firebase/messaging/cpp/MessageWriter;

    invoke-direct {v0}, Lcom/google/firebase/messaging/cpp/MessageWriter;-><init>()V

    sput-object v0, Lcom/google/firebase/messaging/cpp/MessageWriter;->DEFAULT_INSTANCE:Lcom/google/firebase/messaging/cpp/MessageWriter;

    return-void
.end method

.method public constructor <init>()V
    .locals 0

    .line 29
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method

.method public static defaultInstance()Lcom/google/firebase/messaging/cpp/MessageWriter;
    .locals 1

    .line 43
    sget-object v0, Lcom/google/firebase/messaging/cpp/MessageWriter;->DEFAULT_INSTANCE:Lcom/google/firebase/messaging/cpp/MessageWriter;

    return-object v0
.end method

.method private static emptyIfNull(Ljava/lang/String;)Ljava/lang/String;
    .locals 0

    if-eqz p0, :cond_0

    goto :goto_0

    :cond_0
    const-string p0, ""

    :goto_0
    return-object p0
.end method

.method private static generateMessageByteBuffer(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;Lcom/google/firebase/messaging/RemoteMessage$Notification;ZLjava/lang/String;Ljava/lang/String;IIJI)[B
    .locals 33
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;",
            "Lcom/google/firebase/messaging/RemoteMessage$Notification;",
            "Z",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "IIJI)[B"
        }
    .end annotation

    .line 215
    new-instance v2, Lcom/google/flatbuffers/FlatBufferBuilder;

    const/4 v3, 0x0

    invoke-direct {v2, v3}, Lcom/google/flatbuffers/FlatBufferBuilder;-><init>(I)V

    .line 216
    invoke-static/range {p0 .. p0}, Lcom/google/firebase/messaging/cpp/MessageWriter;->emptyIfNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v2, v4}, Lcom/google/flatbuffers/FlatBufferBuilder;->createString(Ljava/lang/CharSequence;)I

    move-result v4

    .line 217
    invoke-static/range {p1 .. p1}, Lcom/google/firebase/messaging/cpp/MessageWriter;->emptyIfNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v2, v5}, Lcom/google/flatbuffers/FlatBufferBuilder;->createString(Ljava/lang/CharSequence;)I

    move-result v5

    .line 218
    invoke-static/range {p2 .. p2}, Lcom/google/firebase/messaging/cpp/MessageWriter;->emptyIfNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v2, v6}, Lcom/google/flatbuffers/FlatBufferBuilder;->createString(Ljava/lang/CharSequence;)I

    move-result v6

    .line 219
    invoke-static/range {p3 .. p3}, Lcom/google/firebase/messaging/cpp/MessageWriter;->emptyIfNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v2, v7}, Lcom/google/flatbuffers/FlatBufferBuilder;->createString(Ljava/lang/CharSequence;)I

    move-result v7

    .line 220
    invoke-static/range {p4 .. p4}, Lcom/google/firebase/messaging/cpp/MessageWriter;->emptyIfNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v8

    invoke-virtual {v2, v8}, Lcom/google/flatbuffers/FlatBufferBuilder;->createString(Ljava/lang/CharSequence;)I

    move-result v8

    .line 221
    invoke-static/range {p8 .. p8}, Lcom/google/firebase/messaging/cpp/MessageWriter;->emptyIfNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v9

    invoke-virtual {v2, v9}, Lcom/google/flatbuffers/FlatBufferBuilder;->createString(Ljava/lang/CharSequence;)I

    move-result v9

    .line 222
    invoke-static/range {p9 .. p9}, Lcom/google/firebase/messaging/cpp/MessageWriter;->emptyIfNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v10

    invoke-virtual {v2, v10}, Lcom/google/flatbuffers/FlatBufferBuilder;->createString(Ljava/lang/CharSequence;)I

    move-result v10

    .line 223
    invoke-static/range {p10 .. p10}, Lcom/google/firebase/messaging/cpp/MessageWriter;->priorityToString(I)Ljava/lang/String;

    move-result-object v11

    invoke-virtual {v2, v11}, Lcom/google/flatbuffers/FlatBufferBuilder;->createString(Ljava/lang/CharSequence;)I

    move-result v11

    .line 224
    invoke-static/range {p11 .. p11}, Lcom/google/firebase/messaging/cpp/MessageWriter;->priorityToString(I)Ljava/lang/String;

    move-result-object v12

    invoke-virtual {v2, v12}, Lcom/google/flatbuffers/FlatBufferBuilder;->createString(Ljava/lang/CharSequence;)I

    move-result v12

    if-eqz p5, :cond_1

    .line 227
    invoke-interface/range {p5 .. p5}, Ljava/util/Map;->size()I

    move-result v13

    new-array v13, v13, [I

    .line 229
    invoke-interface/range {p5 .. p5}, Ljava/util/Map;->entrySet()Ljava/util/Set;

    move-result-object v14

    invoke-interface {v14}, Ljava/util/Set;->iterator()Ljava/util/Iterator;

    move-result-object v14

    const/4 v15, 0x0

    :goto_0
    invoke-interface {v14}, Ljava/util/Iterator;->hasNext()Z

    move-result v16

    if-eqz v16, :cond_0

    invoke-interface {v14}, Ljava/util/Iterator;->next()Ljava/lang/Object;

    move-result-object v16

    check-cast v16, Ljava/util/Map$Entry;

    .line 230
    invoke-interface/range {v16 .. v16}, Ljava/util/Map$Entry;->getKey()Ljava/lang/Object;

    move-result-object v17

    move-object/from16 v3, v17

    check-cast v3, Ljava/lang/CharSequence;

    invoke-virtual {v2, v3}, Lcom/google/flatbuffers/FlatBufferBuilder;->createString(Ljava/lang/CharSequence;)I

    move-result v3

    .line 231
    invoke-interface/range {v16 .. v16}, Ljava/util/Map$Entry;->getValue()Ljava/lang/Object;

    move-result-object v16

    move-object/from16 v19, v14

    move-object/from16 v14, v16

    check-cast v14, Ljava/lang/CharSequence;

    invoke-virtual {v2, v14}, Lcom/google/flatbuffers/FlatBufferBuilder;->createString(Ljava/lang/CharSequence;)I

    move-result v14

    add-int/lit8 v16, v15, 0x1

    .line 232
    invoke-static {v2, v3, v14}, Lcom/google/firebase/messaging/cpp/DataPair;->createDataPair(Lcom/google/flatbuffers/FlatBufferBuilder;II)I

    move-result v3

    aput v3, v13, v15

    move/from16 v15, v16

    move-object/from16 v14, v19

    const/4 v3, 0x0

    goto :goto_0

    .line 234
    :cond_0
    invoke-static {v2, v13}, Lcom/google/firebase/messaging/cpp/SerializedMessage;->createDataVector(Lcom/google/flatbuffers/FlatBufferBuilder;[I)I

    move-result v3

    goto :goto_1

    :cond_1
    const/4 v3, 0x0

    :goto_1
    if-eqz p6, :cond_6

    .line 238
    invoke-virtual/range {p6 .. p6}, Lcom/google/firebase/messaging/RemoteMessage$Notification;->getTitle()Ljava/lang/String;

    move-result-object v13

    invoke-static {v13}, Lcom/google/firebase/messaging/cpp/MessageWriter;->emptyIfNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v13

    invoke-virtual {v2, v13}, Lcom/google/flatbuffers/FlatBufferBuilder;->createString(Ljava/lang/CharSequence;)I

    move-result v13

    .line 239
    invoke-virtual/range {p6 .. p6}, Lcom/google/firebase/messaging/RemoteMessage$Notification;->getBody()Ljava/lang/String;

    move-result-object v14

    invoke-static {v14}, Lcom/google/firebase/messaging/cpp/MessageWriter;->emptyIfNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v14

    invoke-virtual {v2, v14}, Lcom/google/flatbuffers/FlatBufferBuilder;->createString(Ljava/lang/CharSequence;)I

    move-result v14

    .line 240
    invoke-virtual/range {p6 .. p6}, Lcom/google/firebase/messaging/RemoteMessage$Notification;->getIcon()Ljava/lang/String;

    move-result-object v15

    invoke-static {v15}, Lcom/google/firebase/messaging/cpp/MessageWriter;->emptyIfNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v15

    invoke-virtual {v2, v15}, Lcom/google/flatbuffers/FlatBufferBuilder;->createString(Ljava/lang/CharSequence;)I

    move-result v15

    .line 241
    invoke-virtual/range {p6 .. p6}, Lcom/google/firebase/messaging/RemoteMessage$Notification;->getSound()Ljava/lang/String;

    move-result-object v16

    move/from16 v20, v9

    invoke-static/range {v16 .. v16}, Lcom/google/firebase/messaging/cpp/MessageWriter;->emptyIfNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v9

    invoke-virtual {v2, v9}, Lcom/google/flatbuffers/FlatBufferBuilder;->createString(Ljava/lang/CharSequence;)I

    move-result v9

    const-string v1, ""

    .line 242
    invoke-virtual {v2, v1}, Lcom/google/flatbuffers/FlatBufferBuilder;->createString(Ljava/lang/CharSequence;)I

    move-result v1

    .line 243
    invoke-virtual/range {p6 .. p6}, Lcom/google/firebase/messaging/RemoteMessage$Notification;->getTag()Ljava/lang/String;

    move-result-object v16

    move/from16 v21, v3

    invoke-static/range {v16 .. v16}, Lcom/google/firebase/messaging/cpp/MessageWriter;->emptyIfNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    invoke-virtual {v2, v3}, Lcom/google/flatbuffers/FlatBufferBuilder;->createString(Ljava/lang/CharSequence;)I

    move-result v3

    .line 244
    invoke-virtual/range {p6 .. p6}, Lcom/google/firebase/messaging/RemoteMessage$Notification;->getColor()Ljava/lang/String;

    move-result-object v16

    invoke-static/range {v16 .. v16}, Lcom/google/firebase/messaging/cpp/MessageWriter;->emptyIfNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v0

    invoke-virtual {v2, v0}, Lcom/google/flatbuffers/FlatBufferBuilder;->createString(Ljava/lang/CharSequence;)I

    move-result v0

    .line 245
    invoke-virtual/range {p6 .. p6}, Lcom/google/firebase/messaging/RemoteMessage$Notification;->getClickAction()Ljava/lang/String;

    move-result-object v16

    move/from16 v22, v10

    invoke-static/range {v16 .. v16}, Lcom/google/firebase/messaging/cpp/MessageWriter;->emptyIfNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v10

    invoke-virtual {v2, v10}, Lcom/google/flatbuffers/FlatBufferBuilder;->createString(Ljava/lang/CharSequence;)I

    move-result v10

    .line 246
    invoke-virtual/range {p6 .. p6}, Lcom/google/firebase/messaging/RemoteMessage$Notification;->getChannelId()Ljava/lang/String;

    move-result-object v16

    move/from16 v23, v8

    invoke-static/range {v16 .. v16}, Lcom/google/firebase/messaging/cpp/MessageWriter;->emptyIfNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v8

    invoke-virtual {v2, v8}, Lcom/google/flatbuffers/FlatBufferBuilder;->createString(Ljava/lang/CharSequence;)I

    move-result v8

    .line 248
    invoke-virtual/range {p6 .. p6}, Lcom/google/firebase/messaging/RemoteMessage$Notification;->getBodyLocalizationKey()Ljava/lang/String;

    move-result-object v16

    move/from16 v24, v12

    invoke-static/range {v16 .. v16}, Lcom/google/firebase/messaging/cpp/MessageWriter;->emptyIfNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v12

    invoke-virtual {v2, v12}, Lcom/google/flatbuffers/FlatBufferBuilder;->createString(Ljava/lang/CharSequence;)I

    move-result v12

    move/from16 v25, v11

    .line 250
    invoke-virtual/range {p6 .. p6}, Lcom/google/firebase/messaging/RemoteMessage$Notification;->getBodyLocalizationArgs()[Ljava/lang/String;

    move-result-object v11

    if-eqz v11, :cond_3

    move/from16 v26, v7

    .line 253
    array-length v7, v11

    new-array v7, v7, [I

    move/from16 v27, v6

    .line 254
    array-length v6, v11

    move/from16 v28, v5

    const/4 v5, 0x0

    const/16 v16, 0x0

    :goto_2
    if-ge v5, v6, :cond_2

    move/from16 v29, v6

    aget-object v6, v11, v5

    add-int/lit8 v17, v16, 0x1

    .line 255
    invoke-virtual {v2, v6}, Lcom/google/flatbuffers/FlatBufferBuilder;->createString(Ljava/lang/CharSequence;)I

    move-result v6

    aput v6, v7, v16

    add-int/lit8 v5, v5, 0x1

    move/from16 v16, v17

    move/from16 v6, v29

    goto :goto_2

    .line 258
    :cond_2
    invoke-static {v2, v7}, Lcom/google/firebase/messaging/cpp/SerializedNotification;->createBodyLocArgsVector(Lcom/google/flatbuffers/FlatBufferBuilder;[I)I

    move-result v5

    goto :goto_3

    :cond_3
    move/from16 v28, v5

    move/from16 v27, v6

    move/from16 v26, v7

    const/4 v5, 0x0

    .line 261
    :goto_3
    invoke-virtual/range {p6 .. p6}, Lcom/google/firebase/messaging/RemoteMessage$Notification;->getTitleLocalizationKey()Ljava/lang/String;

    move-result-object v6

    invoke-static {v6}, Lcom/google/firebase/messaging/cpp/MessageWriter;->emptyIfNull(Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    invoke-virtual {v2, v6}, Lcom/google/flatbuffers/FlatBufferBuilder;->createString(Ljava/lang/CharSequence;)I

    move-result v6

    .line 263
    invoke-virtual/range {p6 .. p6}, Lcom/google/firebase/messaging/RemoteMessage$Notification;->getTitleLocalizationArgs()[Ljava/lang/String;

    move-result-object v7

    if-eqz v7, :cond_5

    .line 266
    array-length v11, v7

    new-array v11, v11, [I

    move/from16 v30, v4

    .line 267
    array-length v4, v7

    move/from16 v31, v6

    const/4 v6, 0x0

    const/16 v18, 0x0

    :goto_4
    if-ge v6, v4, :cond_4

    move/from16 v32, v4

    aget-object v4, v7, v6

    add-int/lit8 v16, v18, 0x1

    .line 268
    invoke-virtual {v2, v4}, Lcom/google/flatbuffers/FlatBufferBuilder;->createString(Ljava/lang/CharSequence;)I

    move-result v4

    aput v4, v11, v18

    add-int/lit8 v6, v6, 0x1

    move/from16 v18, v16

    move/from16 v4, v32

    goto :goto_4

    .line 271
    :cond_4
    invoke-static {v2, v11}, Lcom/google/firebase/messaging/cpp/SerializedNotification;->createTitleLocArgsVector(Lcom/google/flatbuffers/FlatBufferBuilder;[I)I

    move-result v4

    goto :goto_5

    :cond_5
    move/from16 v30, v4

    move/from16 v31, v6

    const/4 v4, 0x0

    .line 273
    :goto_5
    invoke-static {v2}, Lcom/google/firebase/messaging/cpp/SerializedNotification;->startSerializedNotification(Lcom/google/flatbuffers/FlatBufferBuilder;)V

    .line 274
    invoke-static {v2, v13}, Lcom/google/firebase/messaging/cpp/SerializedNotification;->addTitle(Lcom/google/flatbuffers/FlatBufferBuilder;I)V

    .line 275
    invoke-static {v2, v14}, Lcom/google/firebase/messaging/cpp/SerializedNotification;->addBody(Lcom/google/flatbuffers/FlatBufferBuilder;I)V

    .line 276
    invoke-static {v2, v15}, Lcom/google/firebase/messaging/cpp/SerializedNotification;->addIcon(Lcom/google/flatbuffers/FlatBufferBuilder;I)V

    .line 277
    invoke-static {v2, v9}, Lcom/google/firebase/messaging/cpp/SerializedNotification;->addSound(Lcom/google/flatbuffers/FlatBufferBuilder;I)V

    .line 278
    invoke-static {v2, v1}, Lcom/google/firebase/messaging/cpp/SerializedNotification;->addBadge(Lcom/google/flatbuffers/FlatBufferBuilder;I)V

    .line 279
    invoke-static {v2, v3}, Lcom/google/firebase/messaging/cpp/SerializedNotification;->addTag(Lcom/google/flatbuffers/FlatBufferBuilder;I)V

    .line 280
    invoke-static {v2, v0}, Lcom/google/firebase/messaging/cpp/SerializedNotification;->addColor(Lcom/google/flatbuffers/FlatBufferBuilder;I)V

    .line 281
    invoke-static {v2, v10}, Lcom/google/firebase/messaging/cpp/SerializedNotification;->addClickAction(Lcom/google/flatbuffers/FlatBufferBuilder;I)V

    .line 282
    invoke-static {v2, v8}, Lcom/google/firebase/messaging/cpp/SerializedNotification;->addAndroidChannelId(Lcom/google/flatbuffers/FlatBufferBuilder;I)V

    .line 283
    invoke-static {v2, v12}, Lcom/google/firebase/messaging/cpp/SerializedNotification;->addBodyLocKey(Lcom/google/flatbuffers/FlatBufferBuilder;I)V

    .line 284
    invoke-static {v2, v5}, Lcom/google/firebase/messaging/cpp/SerializedNotification;->addBodyLocArgs(Lcom/google/flatbuffers/FlatBufferBuilder;I)V

    move/from16 v0, v31

    .line 285
    invoke-static {v2, v0}, Lcom/google/firebase/messaging/cpp/SerializedNotification;->addTitleLocKey(Lcom/google/flatbuffers/FlatBufferBuilder;I)V

    .line 286
    invoke-static {v2, v4}, Lcom/google/firebase/messaging/cpp/SerializedNotification;->addTitleLocArgs(Lcom/google/flatbuffers/FlatBufferBuilder;I)V

    .line 287
    invoke-static {v2}, Lcom/google/firebase/messaging/cpp/SerializedNotification;->endSerializedNotification(Lcom/google/flatbuffers/FlatBufferBuilder;)I

    move-result v3

    goto :goto_6

    :cond_6
    move/from16 v21, v3

    move/from16 v30, v4

    move/from16 v28, v5

    move/from16 v27, v6

    move/from16 v26, v7

    move/from16 v23, v8

    move/from16 v20, v9

    move/from16 v22, v10

    move/from16 v25, v11

    move/from16 v24, v12

    const/4 v3, 0x0

    .line 289
    :goto_6
    invoke-static {v2}, Lcom/google/firebase/messaging/cpp/SerializedMessage;->startSerializedMessage(Lcom/google/flatbuffers/FlatBufferBuilder;)V

    move/from16 v0, v30

    .line 290
    invoke-static {v2, v0}, Lcom/google/firebase/messaging/cpp/SerializedMessage;->addFrom(Lcom/google/flatbuffers/FlatBufferBuilder;I)V

    move/from16 v0, v28

    .line 291
    invoke-static {v2, v0}, Lcom/google/firebase/messaging/cpp/SerializedMessage;->addTo(Lcom/google/flatbuffers/FlatBufferBuilder;I)V

    move/from16 v0, v27

    .line 292
    invoke-static {v2, v0}, Lcom/google/firebase/messaging/cpp/SerializedMessage;->addMessageId(Lcom/google/flatbuffers/FlatBufferBuilder;I)V

    move/from16 v0, v26

    .line 293
    invoke-static {v2, v0}, Lcom/google/firebase/messaging/cpp/SerializedMessage;->addMessageType(Lcom/google/flatbuffers/FlatBufferBuilder;I)V

    move/from16 v0, v25

    .line 294
    invoke-static {v2, v0}, Lcom/google/firebase/messaging/cpp/SerializedMessage;->addPriority(Lcom/google/flatbuffers/FlatBufferBuilder;I)V

    move/from16 v0, v24

    .line 295
    invoke-static {v2, v0}, Lcom/google/firebase/messaging/cpp/SerializedMessage;->addOriginalPriority(Lcom/google/flatbuffers/FlatBufferBuilder;I)V

    move-wide/from16 v0, p12

    .line 296
    invoke-static {v2, v0, v1}, Lcom/google/firebase/messaging/cpp/SerializedMessage;->addSentTime(Lcom/google/flatbuffers/FlatBufferBuilder;J)V

    move/from16 v0, p14

    .line 297
    invoke-static {v2, v0}, Lcom/google/firebase/messaging/cpp/SerializedMessage;->addTimeToLive(Lcom/google/flatbuffers/FlatBufferBuilder;I)V

    move/from16 v0, v23

    .line 298
    invoke-static {v2, v0}, Lcom/google/firebase/messaging/cpp/SerializedMessage;->addError(Lcom/google/flatbuffers/FlatBufferBuilder;I)V

    move/from16 v0, v22

    .line 299
    invoke-static {v2, v0}, Lcom/google/firebase/messaging/cpp/SerializedMessage;->addCollapseKey(Lcom/google/flatbuffers/FlatBufferBuilder;I)V

    if-eqz p5, :cond_7

    move/from16 v0, v21

    .line 301
    invoke-static {v2, v0}, Lcom/google/firebase/messaging/cpp/SerializedMessage;->addData(Lcom/google/flatbuffers/FlatBufferBuilder;I)V

    :cond_7
    if-eqz p6, :cond_8

    .line 304
    invoke-static {v2, v3}, Lcom/google/firebase/messaging/cpp/SerializedMessage;->addNotification(Lcom/google/flatbuffers/FlatBufferBuilder;I)V

    :cond_8
    move/from16 v0, p7

    .line 306
    invoke-static {v2, v0}, Lcom/google/firebase/messaging/cpp/SerializedMessage;->addNotificationOpened(Lcom/google/flatbuffers/FlatBufferBuilder;Z)V

    move/from16 v0, v20

    .line 307
    invoke-static {v2, v0}, Lcom/google/firebase/messaging/cpp/SerializedMessage;->addLink(Lcom/google/flatbuffers/FlatBufferBuilder;I)V

    .line 308
    invoke-static {v2}, Lcom/google/firebase/messaging/cpp/SerializedMessage;->endSerializedMessage(Lcom/google/flatbuffers/FlatBufferBuilder;)I

    move-result v0

    .line 309
    invoke-static {v2}, Lcom/google/firebase/messaging/cpp/SerializedEvent;->startSerializedEvent(Lcom/google/flatbuffers/FlatBufferBuilder;)V

    const/4 v1, 0x1

    .line 310
    invoke-static {v2, v1}, Lcom/google/firebase/messaging/cpp/SerializedEvent;->addEventType(Lcom/google/flatbuffers/FlatBufferBuilder;B)V

    .line 311
    invoke-static {v2, v0}, Lcom/google/firebase/messaging/cpp/SerializedEvent;->addEvent(Lcom/google/flatbuffers/FlatBufferBuilder;I)V

    .line 312
    invoke-static {v2}, Lcom/google/firebase/messaging/cpp/SerializedEvent;->endSerializedEvent(Lcom/google/flatbuffers/FlatBufferBuilder;)I

    move-result v0

    invoke-virtual {v2, v0}, Lcom/google/flatbuffers/FlatBufferBuilder;->finish(I)V

    .line 313
    invoke-virtual {v2}, Lcom/google/flatbuffers/FlatBufferBuilder;->sizedByteArray()[B

    move-result-object v0

    return-object v0
.end method

.method private static priorityToString(I)Ljava/lang/String;
    .locals 0

    packed-switch p0, :pswitch_data_0

    const-string p0, ""

    return-object p0

    :pswitch_0
    const-string p0, "normal"

    return-object p0

    :pswitch_1
    const-string p0, "high"

    return-object p0

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method


# virtual methods
.method public writeMessage(Landroid/content/Context;Lcom/google/firebase/messaging/RemoteMessage;ZLandroid/net/Uri;)V
    .locals 18

    .line 49
    invoke-virtual/range {p2 .. p2}, Lcom/google/firebase/messaging/RemoteMessage;->getFrom()Ljava/lang/String;

    move-result-object v2

    .line 50
    invoke-virtual/range {p2 .. p2}, Lcom/google/firebase/messaging/RemoteMessage;->getTo()Ljava/lang/String;

    move-result-object v3

    .line 51
    invoke-virtual/range {p2 .. p2}, Lcom/google/firebase/messaging/RemoteMessage;->getMessageId()Ljava/lang/String;

    move-result-object v4

    .line 52
    invoke-virtual/range {p2 .. p2}, Lcom/google/firebase/messaging/RemoteMessage;->getMessageType()Ljava/lang/String;

    move-result-object v5

    .line 53
    invoke-virtual/range {p2 .. p2}, Lcom/google/firebase/messaging/RemoteMessage;->getData()Ljava/util/Map;

    move-result-object v7

    .line 54
    invoke-virtual/range {p2 .. p2}, Lcom/google/firebase/messaging/RemoteMessage;->getNotification()Lcom/google/firebase/messaging/RemoteMessage$Notification;

    move-result-object v8

    .line 55
    invoke-virtual/range {p2 .. p2}, Lcom/google/firebase/messaging/RemoteMessage;->getCollapseKey()Ljava/lang/String;

    move-result-object v11

    .line 56
    invoke-virtual/range {p2 .. p2}, Lcom/google/firebase/messaging/RemoteMessage;->getPriority()I

    move-result v12

    .line 57
    invoke-virtual/range {p2 .. p2}, Lcom/google/firebase/messaging/RemoteMessage;->getOriginalPriority()I

    move-result v13

    .line 58
    invoke-virtual/range {p2 .. p2}, Lcom/google/firebase/messaging/RemoteMessage;->getSentTime()J

    move-result-wide v14

    .line 59
    invoke-virtual/range {p2 .. p2}, Lcom/google/firebase/messaging/RemoteMessage;->getTtl()I

    move-result v16

    if-nez p4, :cond_0

    if-eqz v8, :cond_0

    .line 63
    invoke-virtual {v8}, Lcom/google/firebase/messaging/RemoteMessage$Notification;->getLink()Landroid/net/Uri;

    move-result-object v0

    goto :goto_0

    :cond_0
    move-object/from16 v0, p4

    :goto_0
    if-eqz v0, :cond_1

    .line 65
    invoke-virtual {v0}, Landroid/net/Uri;->toString()Ljava/lang/String;

    move-result-object v0

    :goto_1
    move-object v10, v0

    goto :goto_2

    :cond_1
    const/4 v0, 0x0

    goto :goto_1

    :goto_2
    const-string v0, "FIREBASE_MESSAGE_WRITER"

    const-string v1, "onMessageReceived from=%s message_id=%s, data=%s, notification=%s"

    const/4 v6, 0x4

    .line 66
    new-array v6, v6, [Ljava/lang/Object;

    const/4 v9, 0x0

    aput-object v2, v6, v9

    const/4 v9, 0x1

    aput-object v4, v6, v9

    const/4 v9, 0x2

    if-nez v7, :cond_2

    const-string v17, "(null)"

    goto :goto_3

    .line 72
    :cond_2
    invoke-virtual {v7}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v17

    :goto_3
    aput-object v17, v6, v9

    const/4 v9, 0x3

    if-nez v8, :cond_3

    const-string v17, "(null)"

    goto :goto_4

    .line 73
    :cond_3
    invoke-virtual {v8}, Ljava/lang/Object;->toString()Ljava/lang/String;

    move-result-object v17

    :goto_4
    aput-object v17, v6, v9

    .line 68
    invoke-static {v1, v6}, Ljava/lang/String;->format(Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v1

    .line 66
    invoke-static {v0, v1}, Lcom/google/firebase/messaging/cpp/DebugLogging;->log(Ljava/lang/String;Ljava/lang/String;)V

    const/4 v6, 0x0

    move-object/from16 v0, p0

    move-object/from16 v1, p1

    move/from16 v9, p3

    .line 74
    invoke-virtual/range {v0 .. v16}, Lcom/google/firebase/messaging/cpp/MessageWriter;->writeMessageToInternalStorage(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;Lcom/google/firebase/messaging/RemoteMessage$Notification;ZLjava/lang/String;Ljava/lang/String;IIJI)V

    return-void
.end method

.method writeMessageEventToInternalStorage(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 17

    move-object/from16 v0, p0

    move-object/from16 v1, p1

    move-object/from16 v4, p2

    move-object/from16 v5, p3

    const/4 v2, 0x0

    const/4 v3, 0x0

    const/4 v6, 0x0

    const/4 v7, 0x0

    const/4 v8, 0x0

    const/4 v9, 0x0

    const/4 v10, 0x0

    const/4 v11, 0x0

    const/4 v12, 0x0

    const/4 v13, 0x0

    const-wide/16 v14, 0x0

    const/16 v16, 0x0

    .line 95
    invoke-virtual/range {v0 .. v16}, Lcom/google/firebase/messaging/cpp/MessageWriter;->writeMessageToInternalStorage(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;Lcom/google/firebase/messaging/RemoteMessage$Notification;ZLjava/lang/String;Ljava/lang/String;IIJI)V

    return-void
.end method

.method writeMessageToInternalStorage(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;Lcom/google/firebase/messaging/RemoteMessage$Notification;ZLjava/lang/String;Ljava/lang/String;IIJI)V
    .locals 6
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Context;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "Ljava/util/Map<",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            ">;",
            "Lcom/google/firebase/messaging/RemoteMessage$Notification;",
            "Z",
            "Ljava/lang/String;",
            "Ljava/lang/String;",
            "IIJI)V"
        }
    .end annotation

    move-object v0, p1

    .line 139
    invoke-static/range {p2 .. p16}, Lcom/google/firebase/messaging/cpp/MessageWriter;->generateMessageByteBuffer(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;Lcom/google/firebase/messaging/RemoteMessage$Notification;ZLjava/lang/String;Ljava/lang/String;IIJI)[B

    move-result-object v1

    const/4 v2, 0x4

    .line 154
    invoke-static {v2}, Ljava/nio/ByteBuffer;->allocate(I)Ljava/nio/ByteBuffer;

    move-result-object v2

    .line 156
    sget-object v3, Ljava/nio/ByteOrder;->LITTLE_ENDIAN:Ljava/nio/ByteOrder;

    invoke-virtual {v2, v3}, Ljava/nio/ByteBuffer;->order(Ljava/nio/ByteOrder;)Ljava/nio/ByteBuffer;

    .line 157
    array-length v3, v1

    invoke-virtual {v2, v3}, Ljava/nio/ByteBuffer;->putInt(I)Ljava/nio/ByteBuffer;

    const/4 v3, 0x0

    :try_start_0
    const-string v4, "FIREBASE_CLOUD_MESSAGING_LOCKFILE"

    const/4 v5, 0x0

    .line 162
    invoke-virtual {p1, v4, v5}, Landroid/content/Context;->openFileOutput(Ljava/lang/String;I)Ljava/io/FileOutputStream;

    move-result-object v4

    .line 163
    invoke-virtual {v4}, Ljava/io/FileOutputStream;->getChannel()Ljava/nio/channels/FileChannel;

    move-result-object v4

    invoke-virtual {v4}, Ljava/nio/channels/FileChannel;->lock()Ljava/nio/channels/FileLock;

    move-result-object v4
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_1
    .catchall {:try_start_0 .. :try_end_0} :catchall_1

    :try_start_1
    const-string v3, "FIREBASE_CLOUD_MESSAGING_LOCAL_STORAGE"

    const v5, 0x8000

    .line 165
    invoke-virtual {p1, v3, v5}, Landroid/content/Context;->openFileOutput(Ljava/lang/String;I)Ljava/io/FileOutputStream;

    move-result-object v0

    .line 168
    invoke-virtual {v2}, Ljava/nio/ByteBuffer;->array()[B

    move-result-object v2

    invoke-virtual {v0, v2}, Ljava/io/FileOutputStream;->write([B)V

    .line 169
    invoke-virtual {v0, v1}, Ljava/io/FileOutputStream;->write([B)V

    .line 170
    invoke-virtual {v0}, Ljava/io/FileOutputStream;->close()V
    :try_end_1
    .catch Ljava/lang/Exception; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    if-eqz v4, :cond_0

    .line 177
    :try_start_2
    invoke-virtual {v4}, Ljava/nio/channels/FileLock;->release()V
    :try_end_2
    .catch Ljava/lang/Exception; {:try_start_2 .. :try_end_2} :catch_2

    goto :goto_1

    :catchall_0
    move-exception v0

    move-object v1, v0

    goto :goto_2

    :catch_0
    move-exception v0

    move-object v3, v4

    goto :goto_0

    :catchall_1
    move-exception v0

    move-object v1, v0

    move-object v4, v3

    goto :goto_2

    :catch_1
    move-exception v0

    .line 172
    :goto_0
    :try_start_3
    invoke-virtual {v0}, Ljava/lang/Exception;->printStackTrace()V
    :try_end_3
    .catchall {:try_start_3 .. :try_end_3} :catchall_1

    if-eqz v3, :cond_0

    .line 177
    :try_start_4
    invoke-virtual {v3}, Ljava/nio/channels/FileLock;->release()V
    :try_end_4
    .catch Ljava/lang/Exception; {:try_start_4 .. :try_end_4} :catch_2

    goto :goto_1

    :catch_2
    move-exception v0

    move-object v1, v0

    .line 180
    invoke-virtual {v1}, Ljava/lang/Exception;->printStackTrace()V

    :cond_0
    :goto_1
    return-void

    :goto_2
    if-eqz v4, :cond_1

    .line 177
    :try_start_5
    invoke-virtual {v4}, Ljava/nio/channels/FileLock;->release()V
    :try_end_5
    .catch Ljava/lang/Exception; {:try_start_5 .. :try_end_5} :catch_3

    goto :goto_3

    :catch_3
    move-exception v0

    move-object v2, v0

    .line 180
    invoke-virtual {v2}, Ljava/lang/Exception;->printStackTrace()V

    .line 181
    :cond_1
    :goto_3
    throw v1
.end method
