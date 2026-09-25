.class public Ljp/colopl/drapro/GogameFcmListenerService;
.super Lcom/google/firebase/messaging/FirebaseMessagingService;
.source "GogameFcmListenerService.java"


# static fields
.field public static final NOTIFICATION_ID:Ljava/lang/String; = "notifyid"

.field public static final NOTIFICATION_TAG:Ljava/lang/String; = "notifytag"

.field private static final TAG:Ljava/lang/String; = "GogameFcmListenerService"


# direct methods
.method public constructor <init>()V
    .locals 0

    .line 40
    invoke-direct {p0}, Lcom/google/firebase/messaging/FirebaseMessagingService;-><init>()V

    return-void
.end method

.method private convertBigPicture(Landroid/graphics/Bitmap;)Landroid/graphics/Bitmap;
    .locals 12

    if-nez p1, :cond_0

    const/4 p1, 0x0

    return-object p1

    .line 287
    :cond_0
    invoke-virtual {p1}, Landroid/graphics/Bitmap;->getHeight()I

    move-result v8

    .line 288
    invoke-virtual {p1}, Landroid/graphics/Bitmap;->getWidth()I

    move-result v9

    .line 289
    invoke-virtual {p1}, Landroid/graphics/Bitmap;->getConfig()Landroid/graphics/Bitmap$Config;

    move-result-object v10

    mul-int v0, v9, v8

    .line 291
    new-array v11, v0, [I

    const/4 v2, 0x0

    const/4 v4, 0x0

    const/4 v5, 0x0

    move-object v0, p1

    move-object v1, v11

    move v3, v9

    move v6, v9

    move v7, v8

    .line 292
    invoke-virtual/range {v0 .. v7}, Landroid/graphics/Bitmap;->getPixels([IIIIIII)V

    int-to-float p1, v9

    int-to-float v0, v8

    const v1, 0x3fe66666    # 1.8f

    mul-float v0, v0, v1

    cmpl-float v2, p1, v0

    if-lez v2, :cond_1

    div-float/2addr p1, v1

    float-to-int p1, p1

    .line 297
    invoke-static {v9, p1, v10}, Landroid/graphics/Bitmap;->createBitmap(IILandroid/graphics/Bitmap$Config;)Landroid/graphics/Bitmap;

    move-result-object p1

    const/4 v2, 0x0

    const/4 v4, 0x0

    .line 300
    invoke-virtual {p1}, Landroid/graphics/Bitmap;->getHeight()I

    move-result v0

    sub-int/2addr v0, v8

    div-int/lit8 v5, v0, 0x2

    move-object v0, p1

    move-object v1, v11

    move v3, v9

    move v6, v9

    move v7, v8

    .line 299
    invoke-virtual/range {v0 .. v7}, Landroid/graphics/Bitmap;->setPixels([IIIIIII)V

    goto :goto_0

    :cond_1
    float-to-int p1, v0

    .line 302
    invoke-static {p1, v8, v10}, Landroid/graphics/Bitmap;->createBitmap(IILandroid/graphics/Bitmap$Config;)Landroid/graphics/Bitmap;

    move-result-object p1

    const/4 v2, 0x0

    .line 305
    invoke-virtual {p1}, Landroid/graphics/Bitmap;->getWidth()I

    move-result v0

    sub-int/2addr v0, v9

    div-int/lit8 v4, v0, 0x2

    const/4 v5, 0x0

    move-object v0, p1

    move-object v1, v11

    move v3, v9

    move v6, v9

    move v7, v8

    .line 304
    invoke-virtual/range {v0 .. v7}, Landroid/graphics/Bitmap;->setPixels([IIIIIII)V

    :goto_0
    return-object p1
.end method

.method private createNotification(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ILandroid/graphics/Bitmap;Landroid/graphics/Bitmap;Landroid/app/PendingIntent;)Landroid/app/Notification;
    .locals 1

    .line 317
    new-instance v0, Landroidx/core/app/NotificationCompat$Builder;

    invoke-direct {v0, p0}, Landroidx/core/app/NotificationCompat$Builder;-><init>(Landroid/content/Context;)V

    .line 318
    invoke-static {p1}, Landroid/text/Html;->fromHtml(Ljava/lang/String;)Landroid/text/Spanned;

    move-result-object p1

    invoke-virtual {v0, p1}, Landroidx/core/app/NotificationCompat$Builder;->setContentTitle(Ljava/lang/CharSequence;)Landroidx/core/app/NotificationCompat$Builder;

    move-result-object p1

    .line 319
    invoke-static {p2}, Landroid/text/Html;->fromHtml(Ljava/lang/String;)Landroid/text/Spanned;

    move-result-object v0

    invoke-virtual {p1, v0}, Landroidx/core/app/NotificationCompat$Builder;->setContentText(Ljava/lang/CharSequence;)Landroidx/core/app/NotificationCompat$Builder;

    move-result-object p1

    .line 320
    invoke-static {p3}, Landroid/text/Html;->fromHtml(Ljava/lang/String;)Landroid/text/Spanned;

    move-result-object p3

    invoke-virtual {p1, p3}, Landroidx/core/app/NotificationCompat$Builder;->setTicker(Ljava/lang/CharSequence;)Landroidx/core/app/NotificationCompat$Builder;

    move-result-object p1

    .line 321
    invoke-virtual {p1, p4}, Landroidx/core/app/NotificationCompat$Builder;->setSmallIcon(I)Landroidx/core/app/NotificationCompat$Builder;

    move-result-object p1

    .line 322
    invoke-virtual {p1, p5}, Landroidx/core/app/NotificationCompat$Builder;->setLargeIcon(Landroid/graphics/Bitmap;)Landroidx/core/app/NotificationCompat$Builder;

    move-result-object p1

    const/4 p3, 0x1

    .line 323
    invoke-virtual {p1, p3}, Landroidx/core/app/NotificationCompat$Builder;->setAutoCancel(Z)Landroidx/core/app/NotificationCompat$Builder;

    move-result-object p1

    .line 324
    invoke-virtual {p1, p7}, Landroidx/core/app/NotificationCompat$Builder;->setContentIntent(Landroid/app/PendingIntent;)Landroidx/core/app/NotificationCompat$Builder;

    move-result-object p1

    if-nez p6, :cond_0

    .line 327
    invoke-virtual {p1}, Landroidx/core/app/NotificationCompat$Builder;->build()Landroid/app/Notification;

    move-result-object p1

    goto :goto_0

    .line 329
    :cond_0
    new-instance p3, Landroidx/core/app/NotificationCompat$BigPictureStyle;

    invoke-direct {p3, p1}, Landroidx/core/app/NotificationCompat$BigPictureStyle;-><init>(Landroidx/core/app/NotificationCompat$Builder;)V

    .line 330
    invoke-virtual {p3, p6}, Landroidx/core/app/NotificationCompat$BigPictureStyle;->bigPicture(Landroid/graphics/Bitmap;)Landroidx/core/app/NotificationCompat$BigPictureStyle;

    move-result-object p1

    invoke-static {p2}, Landroid/text/Html;->fromHtml(Ljava/lang/String;)Landroid/text/Spanned;

    move-result-object p2

    invoke-virtual {p1, p2}, Landroidx/core/app/NotificationCompat$BigPictureStyle;->setSummaryText(Ljava/lang/CharSequence;)Landroidx/core/app/NotificationCompat$BigPictureStyle;

    move-result-object p1

    .line 331
    invoke-virtual {p1}, Landroidx/core/app/NotificationCompat$BigPictureStyle;->build()Landroid/app/Notification;

    move-result-object p1

    :goto_0
    return-object p1
.end method

.method private createPendingIntent(Ljava/lang/String;Ljava/lang/String;)Landroid/app/PendingIntent;
    .locals 5

    const-string v0, "?openurl=true"

    .line 253
    invoke-virtual {p1, v0}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result v0

    if-nez v0, :cond_0

    const-string v0, "http://"

    const-string v1, "gogamedrapro://"

    .line 254
    invoke-virtual {p1, v0, v1}, Ljava/lang/String;->replaceFirst(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object p1

    .line 255
    :cond_0
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v0

    .line 256
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v3, "?"

    invoke-virtual {p1, v3}, Ljava/lang/String;->contains(Ljava/lang/CharSequence;)Z

    move-result p1

    if-eqz p1, :cond_1

    const-string p1, "&"

    goto :goto_0

    :cond_1
    const-string p1, "?"

    :goto_0
    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string p1, "rt="

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, v0, v1}, Ljava/lang/StringBuilder;->append(J)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    .line 257
    new-instance v0, Landroid/content/Intent;

    const-string v1, "android.intent.action.VIEW"

    invoke-static {p1}, Landroid/net/Uri;->parse(Ljava/lang/String;)Landroid/net/Uri;

    move-result-object p1

    invoke-direct {v0, v1, p1}, Landroid/content/Intent;-><init>(Ljava/lang/String;Landroid/net/Uri;)V

    const-string p1, "notifyid"

    .line 258
    invoke-virtual {p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    const-string v2, "app_name"

    const-string v3, "string"

    invoke-virtual {p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getPackageName()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v1, v2, v3, v4}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v1

    invoke-virtual {v0, p1, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;I)Landroid/content/Intent;

    const-string p1, "notifytag"

    .line 259
    invoke-virtual {v0, p1, p2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Ljava/lang/String;)Landroid/content/Intent;

    .line 260
    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide p1

    const-wide/32 v1, 0xfffffff

    and-long/2addr p1, v1

    long-to-int p1, p1

    const/high16 p2, 0x8000000

    .line 261
    invoke-static {p0, p1, v0, p2}, Landroid/app/PendingIntent;->getActivity(Landroid/content/Context;ILandroid/content/Intent;I)Landroid/app/PendingIntent;

    move-result-object p1

    return-object p1
.end method

.method private getContentView(Landroid/content/Context;Lorg/json/JSONObject;)Landroid/widget/RemoteViews;
    .locals 6

    .line 178
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "notification_type"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v1, "layout"

    const/4 v2, -0x1

    invoke-virtual {p2, v1, v2}, Lorg/json/JSONObject;->optInt(Ljava/lang/String;I)I

    move-result v1

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    const-string v1, "layout"

    invoke-direct {p0, p1, v0, v1}, Ljp/colopl/drapro/GogameFcmListenerService;->getIdentifier(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)I

    move-result p1

    const/4 v0, 0x0

    if-nez p1, :cond_0

    return-object v0

    .line 182
    :cond_0
    new-instance v1, Landroid/widget/RemoteViews;

    invoke-virtual {p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getPackageName()Ljava/lang/String;

    move-result-object v2

    invoke-direct {v1, v2, p1}, Landroid/widget/RemoteViews;-><init>(Ljava/lang/String;I)V

    const-string v2, "layout_inflater"

    .line 184
    invoke-virtual {p0, v2}, Ljp/colopl/drapro/GogameFcmListenerService;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v2

    check-cast v2, Landroid/view/LayoutInflater;

    .line 185
    invoke-virtual {v2, p1, v0}, Landroid/view/LayoutInflater;->inflate(ILandroid/view/ViewGroup;)Landroid/view/View;

    move-result-object p1

    .line 187
    invoke-virtual {p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    const-string v3, "img"

    const-string v4, "id"

    invoke-virtual {p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getPackageName()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v2, v3, v4, v5}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v2

    .line 188
    invoke-virtual {p1, v2}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v3

    const/4 v4, 0x1

    if-eqz v3, :cond_3

    const-string v3, "img"

    const-string v5, ""

    .line 189
    invoke-virtual {p2, v3, v5}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    const-string v5, ""

    .line 190
    invoke-virtual {v3, v5}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v5

    if-eqz v5, :cond_1

    return-object v0

    .line 196
    :cond_1
    :try_start_0
    new-instance v5, Ljava/net/URL;

    invoke-direct {v5, v3}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    .line 198
    invoke-virtual {v5}, Ljava/net/URL;->openConnection()Ljava/net/URLConnection;

    move-result-object v3

    check-cast v3, Ljava/net/HttpURLConnection;

    .line 199
    invoke-virtual {v3, v4}, Ljava/net/HttpURLConnection;->setDoInput(Z)V

    .line 200
    invoke-virtual {v3}, Ljava/net/HttpURLConnection;->connect()V

    .line 201
    invoke-virtual {v3}, Ljava/net/HttpURLConnection;->getInputStream()Ljava/io/InputStream;

    move-result-object v3

    .line 202
    invoke-static {v3}, Landroid/graphics/BitmapFactory;->decodeStream(Ljava/io/InputStream;)Landroid/graphics/Bitmap;

    move-result-object v3
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception v3

    .line 204
    invoke-virtual {v3}, Ljava/io/IOException;->printStackTrace()V

    move-object v3, v0

    :goto_0
    if-nez v3, :cond_2

    return-object v0

    .line 209
    :cond_2
    invoke-virtual {v1, v2, v3}, Landroid/widget/RemoteViews;->setImageViewBitmap(ILandroid/graphics/Bitmap;)V

    .line 212
    :cond_3
    invoke-virtual {p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    const-string v2, "title"

    const-string v3, "id"

    invoke-virtual {p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getPackageName()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v0, v2, v3, v5}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    .line 213
    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v2

    if-eqz v2, :cond_4

    const-string v2, "title"

    const-string v3, ""

    .line 215
    invoke-virtual {p2, v2, v3}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-static {v2}, Landroid/text/Html;->fromHtml(Ljava/lang/String;)Landroid/text/Spanned;

    move-result-object v2

    .line 214
    invoke-virtual {v1, v0, v2}, Landroid/widget/RemoteViews;->setTextViewText(ILjava/lang/CharSequence;)V

    .line 218
    :cond_4
    invoke-virtual {p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    const-string v2, "message"

    const-string v3, "id"

    invoke-virtual {p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getPackageName()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v0, v2, v3, v5}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    .line 219
    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v2

    if-eqz v2, :cond_5

    const-string v2, "message"

    const-string v3, ""

    .line 221
    invoke-virtual {p2, v2, v3}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    invoke-static {v2}, Landroid/text/Html;->fromHtml(Ljava/lang/String;)Landroid/text/Spanned;

    move-result-object v2

    .line 220
    invoke-virtual {v1, v0, v2}, Landroid/widget/RemoteViews;->setTextViewText(ILjava/lang/CharSequence;)V

    .line 224
    :cond_5
    invoke-virtual {p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    const-string v2, "time"

    const-string v3, "id"

    invoke-virtual {p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getPackageName()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v0, v2, v3, v5}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    .line 225
    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object v2

    if-eqz v2, :cond_7

    const-string v2, "time"

    .line 226
    invoke-virtual {p2, v2, v4}, Lorg/json/JSONObject;->optBoolean(Ljava/lang/String;Z)Z

    move-result v2

    if-eqz v2, :cond_6

    .line 227
    new-instance v2, Ljava/util/Date;

    invoke-direct {v2}, Ljava/util/Date;-><init>()V

    .line 228
    new-instance v3, Ljava/text/SimpleDateFormat;

    const-string v4, "kk\':\'mm"

    invoke-direct {v3, v4}, Ljava/text/SimpleDateFormat;-><init>(Ljava/lang/String;)V

    .line 229
    invoke-virtual {v3, v2}, Ljava/text/SimpleDateFormat;->format(Ljava/util/Date;)Ljava/lang/String;

    move-result-object v2

    invoke-virtual {v1, v0, v2}, Landroid/widget/RemoteViews;->setTextViewText(ILjava/lang/CharSequence;)V

    goto :goto_1

    :cond_6
    const/16 v2, 0x8

    .line 231
    invoke-virtual {v1, v0, v2}, Landroid/widget/RemoteViews;->setViewVisibility(II)V

    .line 235
    :cond_7
    :goto_1
    invoke-virtual {p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    const-string v2, "root"

    const-string v3, "id"

    invoke-virtual {p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getPackageName()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v0, v2, v3, v4}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v0

    .line 236
    invoke-virtual {p1, v0}, Landroid/view/View;->findViewById(I)Landroid/view/View;

    move-result-object p1

    if-eqz p1, :cond_8

    const-string p1, "bgcolor"

    .line 237
    invoke-virtual {p2, p1}, Lorg/json/JSONObject;->has(Ljava/lang/String;)Z

    move-result p1

    if-eqz p1, :cond_8

    .line 238
    new-instance p1, Ljava/lang/StringBuilder;

    invoke-direct {p1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "#"

    invoke-virtual {p1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "bgcolor"

    invoke-virtual {p2, v2}, Lorg/json/JSONObject;->optString(Ljava/lang/String;)Ljava/lang/String;

    move-result-object p2

    invoke-virtual {p1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    const-string p2, "setBackgroundColor"

    .line 240
    invoke-static {p1}, Landroid/graphics/Color;->parseColor(Ljava/lang/String;)I

    move-result p1

    .line 239
    invoke-virtual {v1, v0, p2, p1}, Landroid/widget/RemoteViews;->setInt(ILjava/lang/String;I)V

    :cond_8
    return-object v1
.end method

.method private getIdentifier(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)I
    .locals 1

    .line 249
    invoke-virtual {p1}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    invoke-virtual {p1}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object p1

    invoke-virtual {v0, p2, p3, p1}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p1

    return p1
.end method

.method private getImage(Ljava/lang/String;)Landroid/graphics/Bitmap;
    .locals 1

    .line 268
    :try_start_0
    new-instance v0, Ljava/net/URL;

    invoke-direct {v0, p1}, Ljava/net/URL;-><init>(Ljava/lang/String;)V

    .line 269
    invoke-virtual {v0}, Ljava/net/URL;->openConnection()Ljava/net/URLConnection;

    move-result-object p1

    check-cast p1, Ljava/net/HttpURLConnection;

    const/4 v0, 0x1

    .line 270
    invoke-virtual {p1, v0}, Ljava/net/HttpURLConnection;->setDoInput(Z)V

    .line 271
    invoke-virtual {p1}, Ljava/net/HttpURLConnection;->connect()V

    .line 272
    invoke-virtual {p1}, Ljava/net/HttpURLConnection;->getInputStream()Ljava/io/InputStream;

    move-result-object p1

    .line 273
    invoke-static {p1}, Landroid/graphics/BitmapFactory;->decodeStream(Ljava/io/InputStream;)Landroid/graphics/Bitmap;

    move-result-object p1
    :try_end_0
    .catch Ljava/io/IOException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    .line 275
    invoke-virtual {p1}, Ljava/io/IOException;->printStackTrace()V

    const/4 p1, 0x0

    :goto_0
    return-object p1
.end method

.method private sendNotification(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 6

    const-string v0, "GogameFcmListenerService"

    .line 63
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "sendNotification: subject = "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ", message = "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ", url = "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, ", option = "

    invoke-virtual {v1, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1, p4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    invoke-static {v0, v1}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 66
    invoke-virtual {p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    const-string v1, ""

    .line 68
    invoke-virtual {p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    const-string v3, "push_icon"

    const-string v4, "drawable"

    invoke-virtual {p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getPackageName()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v2, v3, v4, v5}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v2

    const/4 v3, 0x0

    if-eqz p4, :cond_0

    .line 74
    :try_start_0
    new-instance v4, Lorg/json/JSONObject;

    invoke-direct {v4, p4}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-object v4, v3

    :goto_0
    if-eqz v4, :cond_0

    .line 78
    invoke-direct {p0, v0, v4}, Ljp/colopl/drapro/GogameFcmListenerService;->getContentView(Landroid/content/Context;Lorg/json/JSONObject;)Landroid/widget/RemoteViews;

    move-result-object v3

    const-string p4, "tag"

    const-string v1, ""

    .line 80
    invoke-virtual {v4, p4, v1}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v1

    .line 81
    new-instance p4, Ljava/lang/StringBuilder;

    invoke-direct {p4}, Ljava/lang/StringBuilder;-><init>()V

    const-string v2, "ic_notification"

    invoke-virtual {p4, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v2, "icon"

    const/4 v5, -0x1

    invoke-virtual {v4, v2, v5}, Lorg/json/JSONObject;->optInt(Ljava/lang/String;I)I

    move-result v2

    invoke-virtual {p4, v2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {p4}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p4

    const-string v2, "drawable"

    invoke-direct {p0, v0, p4, v2}, Ljp/colopl/drapro/GogameFcmListenerService;->getIdentifier(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)I

    move-result v2

    if-nez v2, :cond_0

    .line 83
    invoke-virtual {p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getApplicationContext()Landroid/content/Context;

    move-result-object p4

    invoke-virtual {p4}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object p4

    const-string v0, "push_icon"

    const-string v2, "drawable"

    invoke-virtual {p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getApplicationContext()Landroid/content/Context;

    move-result-object v4

    invoke-virtual {v4}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v4

    invoke-virtual {p4, v0, v2, v4}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v2

    .line 88
    :cond_0
    invoke-virtual {p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getApplicationContext()Landroid/content/Context;

    move-result-object p4

    invoke-virtual {p4}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object p4

    const-string v0, "notification_title"

    const-string v4, "string"

    invoke-virtual {p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getApplicationContext()Landroid/content/Context;

    move-result-object v5

    invoke-virtual {v5}, Landroid/content/Context;->getPackageName()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {p4, v0, v4, v5}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p4

    .line 89
    new-instance v0, Landroid/app/Notification$Builder;

    invoke-direct {v0, p0}, Landroid/app/Notification$Builder;-><init>(Landroid/content/Context;)V

    const/4 v4, 0x0

    .line 90
    invoke-virtual {v0, v4}, Landroid/app/Notification$Builder;->setAutoCancel(Z)Landroid/app/Notification$Builder;

    move-result-object v4

    invoke-virtual {v4, v2}, Landroid/app/Notification$Builder;->setSmallIcon(I)Landroid/app/Notification$Builder;

    move-result-object v2

    invoke-virtual {v2, p1}, Landroid/app/Notification$Builder;->setTicker(Ljava/lang/CharSequence;)Landroid/app/Notification$Builder;

    move-result-object p1

    invoke-virtual {p0, p4}, Ljp/colopl/drapro/GogameFcmListenerService;->getString(I)Ljava/lang/String;

    move-result-object p4

    invoke-virtual {p1, p4}, Landroid/app/Notification$Builder;->setContentTitle(Ljava/lang/CharSequence;)Landroid/app/Notification$Builder;

    move-result-object p1

    invoke-virtual {p1, p2}, Landroid/app/Notification$Builder;->setContentText(Ljava/lang/CharSequence;)Landroid/app/Notification$Builder;

    move-result-object p1

    invoke-direct {p0, p3, v1}, Ljp/colopl/drapro/GogameFcmListenerService;->createPendingIntent(Ljava/lang/String;Ljava/lang/String;)Landroid/app/PendingIntent;

    move-result-object p2

    invoke-virtual {p1, p2}, Landroid/app/Notification$Builder;->setContentIntent(Landroid/app/PendingIntent;)Landroid/app/Notification$Builder;

    if-eqz v3, :cond_1

    .line 92
    invoke-virtual {v0, v3}, Landroid/app/Notification$Builder;->setContent(Landroid/widget/RemoteViews;)Landroid/app/Notification$Builder;

    :cond_1
    const-string p1, "notification"

    .line 95
    invoke-virtual {p0, p1}, Ljp/colopl/drapro/GogameFcmListenerService;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Landroid/app/NotificationManager;

    .line 96
    invoke-virtual {p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getResources()Landroid/content/res/Resources;

    move-result-object p2

    const-string p3, "app_name"

    const-string p4, "string"

    invoke-virtual {p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getPackageName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p2, p3, p4, v2}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p2

    invoke-virtual {p1, v1, p2}, Landroid/app/NotificationManager;->cancel(Ljava/lang/String;I)V

    .line 97
    invoke-virtual {p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getResources()Landroid/content/res/Resources;

    move-result-object p2

    const-string p3, "app_name"

    const-string p4, "string"

    invoke-virtual {p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getPackageName()Ljava/lang/String;

    move-result-object v2

    invoke-virtual {p2, p3, p4, v2}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result p2

    invoke-virtual {v0}, Landroid/app/Notification$Builder;->build()Landroid/app/Notification;

    move-result-object p3

    invoke-virtual {p1, v1, p2, p3}, Landroid/app/NotificationManager;->notify(Ljava/lang/String;ILandroid/app/Notification;)V

    return-void
.end method

.method private sendNotificationV2(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    .locals 16

    move-object/from16 v8, p0

    move-object/from16 v0, p3

    move-object/from16 v1, p4

    const-string v2, "GogameFcmListenerService"

    .line 101
    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    const-string v4, "sendNotificationV2: subject = "

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-object/from16 v4, p1

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v5, ", message = "

    invoke-virtual {v3, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    move-object/from16 v5, p2

    invoke-virtual {v3, v5}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v6, ", url = "

    invoke-virtual {v3, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v6, ", option = "

    invoke-virtual {v3, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-static {v2, v3}, Ljp/colopl/util/Util;->dLog(Ljava/lang/String;Ljava/lang/String;)V

    .line 104
    invoke-virtual/range {p0 .. p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    const-string v3, "notification_title"

    const-string v6, "string"

    invoke-virtual/range {p0 .. p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getPackageName()Ljava/lang/String;

    move-result-object v7

    invoke-virtual {v2, v3, v6, v7}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v2

    .line 105
    invoke-virtual/range {p0 .. p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getApplicationContext()Landroid/content/Context;

    move-result-object v3

    const-string v6, ""

    .line 108
    invoke-virtual/range {p0 .. p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getResources()Landroid/content/res/Resources;

    move-result-object v7

    const-string v9, "push_icon"

    const-string v10, "drawable"

    invoke-virtual/range {p0 .. p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getPackageName()Ljava/lang/String;

    move-result-object v11

    invoke-virtual {v7, v9, v10, v11}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v7

    .line 109
    invoke-virtual {v8, v2}, Ljp/colopl/drapro/GogameFcmListenerService;->getString(I)Ljava/lang/String;

    move-result-object v2

    const/4 v9, 0x0

    const/4 v10, 0x0

    if-eqz v1, :cond_2

    .line 118
    :try_start_0
    new-instance v11, Lorg/json/JSONObject;

    invoke-direct {v11, v1}, Lorg/json/JSONObject;-><init>(Ljava/lang/String;)V
    :try_end_0
    .catch Lorg/json/JSONException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-object v11, v10

    :goto_0
    if-eqz v11, :cond_1

    const-string v1, "layout"

    .line 122
    invoke-virtual {v11, v1, v9}, Lorg/json/JSONObject;->optInt(Ljava/lang/String;I)I

    move-result v9

    const-string v1, "tag"

    const-string v6, ""

    .line 123
    invoke-virtual {v11, v1, v6}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    .line 124
    new-instance v1, Ljava/lang/StringBuilder;

    invoke-direct {v1}, Ljava/lang/StringBuilder;-><init>()V

    const-string v7, "ic_notification"

    invoke-virtual {v1, v7}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    const-string v7, "icon"

    const/4 v12, -0x1

    invoke-virtual {v11, v7, v12}, Lorg/json/JSONObject;->optInt(Ljava/lang/String;I)I

    move-result v7

    invoke-virtual {v1, v7}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v1}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v1

    const-string v7, "drawable"

    invoke-direct {v8, v3, v1, v7}, Ljp/colopl/drapro/GogameFcmListenerService;->getIdentifier(Landroid/content/Context;Ljava/lang/String;Ljava/lang/String;)I

    move-result v1

    if-nez v1, :cond_0

    .line 127
    invoke-virtual/range {p0 .. p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getResources()Landroid/content/res/Resources;

    move-result-object v1

    const-string v3, "push_icon"

    const-string v7, "drawable"

    invoke-virtual/range {p0 .. p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getPackageName()Ljava/lang/String;

    move-result-object v12

    invoke-virtual {v1, v3, v7, v12}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v1

    :cond_0
    move v7, v1

    :cond_1
    move-object v12, v6

    goto :goto_1

    :cond_2
    move-object v12, v6

    move-object v11, v10

    :goto_1
    const/4 v1, 0x1

    if-eq v9, v1, :cond_8

    const/4 v3, 0x2

    if-ne v9, v3, :cond_3

    goto/16 :goto_4

    :cond_3
    const/4 v3, 0x3

    if-ne v9, v3, :cond_4

    const-string v2, "title"

    const-string v3, ""

    .line 143
    invoke-virtual {v11, v2, v3}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    const-string v3, "message"

    const-string v5, ""

    .line 144
    invoke-virtual {v11, v3, v5}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    .line 145
    invoke-virtual/range {p0 .. p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getResources()Landroid/content/res/Resources;

    move-result-object v5

    invoke-virtual/range {p0 .. p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getResources()Landroid/content/res/Resources;

    move-result-object v6

    const-string v9, "app_icon"

    const-string v13, "drawable"

    invoke-virtual/range {p0 .. p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getPackageName()Ljava/lang/String;

    move-result-object v14

    invoke-virtual {v6, v9, v13, v14}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v6

    invoke-static {v5, v6}, Landroid/graphics/BitmapFactory;->decodeResource(Landroid/content/res/Resources;I)Landroid/graphics/Bitmap;

    move-result-object v5

    const-string v6, "img"

    const-string v9, ""

    .line 146
    invoke-virtual {v11, v6, v9}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    const-string v9, ""

    .line 147
    invoke-virtual {v6, v9}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v9

    if-nez v9, :cond_a

    .line 148
    invoke-direct {v8, v6}, Ljp/colopl/drapro/GogameFcmListenerService;->getImage(Ljava/lang/String;)Landroid/graphics/Bitmap;

    move-result-object v6

    invoke-direct {v8, v6}, Ljp/colopl/drapro/GogameFcmListenerService;->convertBigPicture(Landroid/graphics/Bitmap;)Landroid/graphics/Bitmap;

    move-result-object v10

    goto/16 :goto_6

    :cond_4
    const/4 v3, 0x4

    if-eq v9, v3, :cond_7

    const/4 v3, 0x5

    if-ne v9, v3, :cond_5

    goto :goto_3

    .line 157
    :cond_5
    invoke-virtual/range {p0 .. p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getResources()Landroid/content/res/Resources;

    move-result-object v3

    invoke-virtual/range {p0 .. p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getResources()Landroid/content/res/Resources;

    move-result-object v6

    const-string v9, "app_icon"

    const-string v11, "drawable"

    invoke-virtual/range {p0 .. p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getPackageName()Ljava/lang/String;

    move-result-object v13

    invoke-virtual {v6, v9, v11, v13}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v6

    invoke-static {v3, v6}, Landroid/graphics/BitmapFactory;->decodeResource(Landroid/content/res/Resources;I)Landroid/graphics/Bitmap;

    move-result-object v3

    :cond_6
    :goto_2
    move-object v6, v10

    goto/16 :goto_7

    .line 151
    :cond_7
    :goto_3
    invoke-virtual/range {p0 .. p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getResources()Landroid/content/res/Resources;

    move-result-object v3

    invoke-virtual/range {p0 .. p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getResources()Landroid/content/res/Resources;

    move-result-object v6

    const-string v9, "app_icon"

    const-string v13, "drawable"

    invoke-virtual/range {p0 .. p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getPackageName()Ljava/lang/String;

    move-result-object v14

    invoke-virtual {v6, v9, v13, v14}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v6

    invoke-static {v3, v6}, Landroid/graphics/BitmapFactory;->decodeResource(Landroid/content/res/Resources;I)Landroid/graphics/Bitmap;

    move-result-object v3

    const-string v6, "img"

    const-string v9, ""

    .line 152
    invoke-virtual {v11, v6, v9}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v6

    const-string v9, ""

    .line 153
    invoke-virtual {v6, v9}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v9

    if-nez v9, :cond_6

    .line 154
    invoke-direct {v8, v6}, Ljp/colopl/drapro/GogameFcmListenerService;->getImage(Ljava/lang/String;)Landroid/graphics/Bitmap;

    move-result-object v6

    invoke-direct {v8, v6}, Ljp/colopl/drapro/GogameFcmListenerService;->convertBigPicture(Landroid/graphics/Bitmap;)Landroid/graphics/Bitmap;

    move-result-object v10

    goto :goto_2

    :cond_8
    :goto_4
    const-string v2, "title"

    const-string v3, ""

    .line 133
    invoke-virtual {v11, v2, v3}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v2

    const-string v3, "message"

    const-string v5, ""

    .line 134
    invoke-virtual {v11, v3, v5}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v3

    const-string v5, "img"

    const-string v6, ""

    .line 135
    invoke-virtual {v11, v5, v6}, Lorg/json/JSONObject;->optString(Ljava/lang/String;Ljava/lang/String;)Ljava/lang/String;

    move-result-object v5

    const-string v6, ""

    .line 136
    invoke-virtual {v5, v6}, Ljava/lang/String;->equals(Ljava/lang/Object;)Z

    move-result v6

    if-nez v6, :cond_9

    .line 137
    invoke-direct {v8, v5}, Ljp/colopl/drapro/GogameFcmListenerService;->getImage(Ljava/lang/String;)Landroid/graphics/Bitmap;

    move-result-object v5

    goto :goto_5

    :cond_9
    move-object v5, v10

    :goto_5
    if-nez v5, :cond_a

    .line 140
    invoke-virtual/range {p0 .. p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getResources()Landroid/content/res/Resources;

    move-result-object v5

    invoke-virtual/range {p0 .. p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getResources()Landroid/content/res/Resources;

    move-result-object v6

    const-string v9, "app_icon"

    const-string v11, "drawable"

    invoke-virtual/range {p0 .. p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getPackageName()Ljava/lang/String;

    move-result-object v13

    invoke-virtual {v6, v9, v11, v13}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v6

    invoke-static {v5, v6}, Landroid/graphics/BitmapFactory;->decodeResource(Landroid/content/res/Resources;I)Landroid/graphics/Bitmap;

    move-result-object v5

    :cond_a
    :goto_6
    move-object v6, v10

    move-object v15, v5

    move-object v5, v3

    move-object v3, v15

    .line 161
    :goto_7
    invoke-virtual/range {p0 .. p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getResources()Landroid/content/res/Resources;

    move-result-object v9

    const v10, 0x1050005

    invoke-virtual {v9, v10}, Landroid/content/res/Resources;->getDimensionPixelSize(I)I

    move-result v9

    .line 162
    invoke-virtual/range {p0 .. p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getResources()Landroid/content/res/Resources;

    move-result-object v10

    const v11, 0x1050006

    invoke-virtual {v10, v11}, Landroid/content/res/Resources;->getDimensionPixelSize(I)I

    move-result v10

    .line 163
    invoke-virtual {v3}, Landroid/graphics/Bitmap;->getWidth()I

    move-result v11

    if-gt v11, v9, :cond_c

    invoke-virtual {v3}, Landroid/graphics/Bitmap;->getHeight()I

    move-result v11

    if-le v11, v10, :cond_b

    goto :goto_8

    :cond_b
    move-object v9, v3

    goto :goto_9

    .line 164
    :cond_c
    :goto_8
    invoke-static {v3, v9, v10, v1}, Landroid/graphics/Bitmap;->createScaledBitmap(Landroid/graphics/Bitmap;IIZ)Landroid/graphics/Bitmap;

    move-result-object v1

    move-object v9, v1

    .line 169
    :goto_9
    invoke-direct {v8, v0, v12}, Ljp/colopl/drapro/GogameFcmListenerService;->createPendingIntent(Ljava/lang/String;Ljava/lang/String;)Landroid/app/PendingIntent;

    move-result-object v10

    move-object/from16 v0, p0

    move-object v1, v2

    move-object v2, v5

    move-object/from16 v3, p1

    move v4, v7

    move-object v5, v9

    move-object v7, v10

    .line 167
    invoke-direct/range {v0 .. v7}, Ljp/colopl/drapro/GogameFcmListenerService;->createNotification(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;ILandroid/graphics/Bitmap;Landroid/graphics/Bitmap;Landroid/app/PendingIntent;)Landroid/app/Notification;

    move-result-object v0

    const-string v1, "notification"

    .line 171
    invoke-virtual {v8, v1}, Ljp/colopl/drapro/GogameFcmListenerService;->getSystemService(Ljava/lang/String;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Landroid/app/NotificationManager;

    .line 172
    invoke-virtual/range {p0 .. p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    const-string v3, "app_name"

    const-string v4, "string"

    invoke-virtual/range {p0 .. p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getPackageName()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v2, v3, v4, v5}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v2

    invoke-virtual {v1, v12, v2}, Landroid/app/NotificationManager;->cancel(Ljava/lang/String;I)V

    .line 173
    invoke-virtual/range {p0 .. p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getResources()Landroid/content/res/Resources;

    move-result-object v2

    const-string v3, "app_name"

    const-string v4, "string"

    invoke-virtual/range {p0 .. p0}, Ljp/colopl/drapro/GogameFcmListenerService;->getPackageName()Ljava/lang/String;

    move-result-object v5

    invoke-virtual {v2, v3, v4, v5}, Landroid/content/res/Resources;->getIdentifier(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)I

    move-result v2

    invoke-virtual {v1, v12, v2, v0}, Landroid/app/NotificationManager;->notify(Ljava/lang/String;ILandroid/app/Notification;)V

    return-void
.end method


# virtual methods
.method public onMessageReceived(Lcom/google/firebase/messaging/RemoteMessage;)V
    .locals 5

    .line 48
    :try_start_0
    invoke-virtual {p1}, Lcom/google/firebase/messaging/RemoteMessage;->getData()Ljava/util/Map;

    move-result-object v0

    const-string v1, "title"

    invoke-interface {v0, v1}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Ljava/lang/String;

    .line 49
    invoke-virtual {p1}, Lcom/google/firebase/messaging/RemoteMessage;->getData()Ljava/util/Map;

    move-result-object v1

    const-string v2, "body"

    invoke-interface {v1, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object v1

    check-cast v1, Ljava/lang/String;

    .line 50
    invoke-virtual {p1}, Lcom/google/firebase/messaging/RemoteMessage;->getData()Ljava/util/Map;

    move-result-object p1

    const-string v2, "url"

    invoke-interface {p1, v2}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/lang/String;

    .line 52
    sget v2, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v3, 0x10

    const/4 v4, 0x0

    if-lt v2, v3, :cond_0

    .line 54
    invoke-direct {p0, v0, v1, p1, v4}, Ljp/colopl/drapro/GogameFcmListenerService;->sendNotificationV2(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V

    goto :goto_0

    .line 56
    :cond_0
    invoke-direct {p0, v0, v1, p1, v4}, Ljp/colopl/drapro/GogameFcmListenerService;->sendNotification(Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;Ljava/lang/String;)V
    :try_end_0
    .catch Ljava/lang/Exception; {:try_start_0 .. :try_end_0} :catch_0

    :catch_0
    :goto_0
    return-void
.end method
