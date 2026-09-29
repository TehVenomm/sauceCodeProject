.class public final enum Lcom/zopim/android/sdk/attachment/ImagePicker;
.super Ljava/lang/Enum;


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/zopim/android/sdk/attachment/ImagePicker$Callback;,
        Lcom/zopim/android/sdk/attachment/ImagePicker$b;,
        Lcom/zopim/android/sdk/attachment/ImagePicker$a;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Enum<",
        "Lcom/zopim/android/sdk/attachment/ImagePicker;",
        ">;"
    }
.end annotation


# static fields
.field private static final synthetic $VALUES:[Lcom/zopim/android/sdk/attachment/ImagePicker;

.field private static final CAMERA_REQUEST_ID_END:I = 0x19a8

.field private static final CAMERA_REQUEST_ID_START:I = 0x11d8

.field private static final CONTENT_TYPE:Ljava/lang/String; = "image/*"

.field private static final GALLERY_REQUEST_ID:I = 0x11d7

.field public static final enum INSTANCE:Lcom/zopim/android/sdk/attachment/ImagePicker;

.field private static final LOG_TAG:Ljava/lang/String;


# instance fields
.field private final mCameraImages:Ljava/util/Map;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/util/Map<",
            "Ljava/lang/Integer;",
            "Ljava/io/File;",
            ">;"
        }
    .end annotation
.end field


# direct methods
.method static constructor <clinit>()V
    .locals 3

    new-instance v0, Lcom/zopim/android/sdk/attachment/ImagePicker;

    const-string v1, "INSTANCE"

    const/4 v2, 0x0

    invoke-direct {v0, v1, v2}, Lcom/zopim/android/sdk/attachment/ImagePicker;-><init>(Ljava/lang/String;I)V

    sput-object v0, Lcom/zopim/android/sdk/attachment/ImagePicker;->INSTANCE:Lcom/zopim/android/sdk/attachment/ImagePicker;

    const/4 v0, 0x1

    new-array v0, v0, [Lcom/zopim/android/sdk/attachment/ImagePicker;

    sget-object v1, Lcom/zopim/android/sdk/attachment/ImagePicker;->INSTANCE:Lcom/zopim/android/sdk/attachment/ImagePicker;

    aput-object v1, v0, v2

    sput-object v0, Lcom/zopim/android/sdk/attachment/ImagePicker;->$VALUES:[Lcom/zopim/android/sdk/attachment/ImagePicker;

    const-class v0, Lcom/zopim/android/sdk/attachment/ImagePicker;

    invoke-virtual {v0}, Ljava/lang/Class;->getSimpleName()Ljava/lang/String;

    move-result-object v0

    sput-object v0, Lcom/zopim/android/sdk/attachment/ImagePicker;->LOG_TAG:Ljava/lang/String;

    return-void
.end method

.method private constructor <init>(Ljava/lang/String;I)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "()V"
        }
    .end annotation

    invoke-direct {p0, p1, p2}, Ljava/lang/Enum;-><init>(Ljava/lang/String;I)V

    new-instance p1, Ljava/util/HashMap;

    invoke-direct {p1}, Ljava/util/HashMap;-><init>()V

    iput-object p1, p0, Lcom/zopim/android/sdk/attachment/ImagePicker;->mCameraImages:Ljava/util/Map;

    return-void
.end method

.method static synthetic access$000()Ljava/lang/String;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/attachment/ImagePicker;->LOG_TAG:Ljava/lang/String;

    return-object v0
.end method

.method private answerCallback(Ljava/util/List;Lcom/zopim/android/sdk/attachment/ImagePicker$Callback;)V
    .locals 0
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Ljava/util/List<",
            "Ljava/io/File;",
            ">;",
            "Lcom/zopim/android/sdk/attachment/ImagePicker$Callback;",
            ")V"
        }
    .end annotation

    if-eqz p2, :cond_0

    invoke-interface {p2, p1}, Lcom/zopim/android/sdk/attachment/ImagePicker$Callback;->onSuccess(Ljava/util/List;)V

    :cond_0
    return-void
.end method

.method private extractUrisFromIntent(Landroid/content/Intent;)Ljava/util/List;
    .locals 5
    .annotation build Landroid/annotation/SuppressLint;
        value = {
            "NewApi"
        }
    .end annotation

    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(",
            "Landroid/content/Intent;",
            ")",
            "Ljava/util/List<",
            "Landroid/net/Uri;",
            ">;"
        }
    .end annotation

    new-instance v0, Ljava/util/ArrayList;

    invoke-direct {v0}, Ljava/util/ArrayList;-><init>()V

    sget v1, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v2, 0x10

    if-lt v1, v2, :cond_1

    invoke-virtual {p1}, Landroid/content/Intent;->getClipData()Landroid/content/ClipData;

    move-result-object v1

    if-eqz v1, :cond_1

    invoke-virtual {p1}, Landroid/content/Intent;->getClipData()Landroid/content/ClipData;

    move-result-object p1

    invoke-virtual {p1}, Landroid/content/ClipData;->getItemCount()I

    move-result v1

    const/4 v2, 0x0

    :goto_0
    if-ge v2, v1, :cond_2

    invoke-virtual {p1, v2}, Landroid/content/ClipData;->getItemAt(I)Landroid/content/ClipData$Item;

    move-result-object v3

    invoke-virtual {v3}, Landroid/content/ClipData$Item;->getUri()Landroid/net/Uri;

    move-result-object v4

    if-eqz v4, :cond_0

    invoke-virtual {v3}, Landroid/content/ClipData$Item;->getUri()Landroid/net/Uri;

    move-result-object v3

    invoke-interface {v0, v3}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :cond_0
    add-int/lit8 v2, v2, 0x1

    goto :goto_0

    :cond_1
    invoke-virtual {p1}, Landroid/content/Intent;->getData()Landroid/net/Uri;

    move-result-object v1

    if-eqz v1, :cond_2

    invoke-virtual {p1}, Landroid/content/Intent;->getData()Landroid/net/Uri;

    move-result-object p1

    invoke-interface {v0, p1}, Ljava/util/List;->add(Ljava/lang/Object;)Z

    :cond_2
    return-object v0
.end method

.method private hasExternalReadPermission(Landroid/content/Context;)Z
    .locals 4
    .annotation build Landroid/annotation/TargetApi;
        value = 0x13
    .end annotation

    sget v0, Landroid/os/Build$VERSION;->SDK_INT:I

    const/4 v1, 0x0

    const/4 v2, 0x1

    const/16 v3, 0x13

    if-lt v0, v3, :cond_1

    const-string v0, "android.permission.READ_EXTERNAL_STORAGE"

    invoke-virtual {p1, v0}, Landroid/content/Context;->checkCallingOrSelfPermission(Ljava/lang/String;)I

    move-result p1

    if-nez p1, :cond_0

    const/4 v1, 0x1

    :cond_0
    return v1

    :cond_1
    const-string v0, "android.permission.WRITE_EXTERNAL_STORAGE"

    invoke-virtual {p1, v0}, Landroid/content/Context;->checkCallingOrSelfPermission(Ljava/lang/String;)I

    move-result p1

    if-nez p1, :cond_2

    const/4 v1, 0x1

    :cond_2
    return v1
.end method

.method private pickImageFromCameraInternal(Landroidx/fragment/app/Fragment;)V
    .locals 10

    iget-object v0, p0, Lcom/zopim/android/sdk/attachment/ImagePicker;->mCameraImages:Ljava/util/Map;

    invoke-interface {v0}, Ljava/util/Map;->keySet()Ljava/util/Set;

    move-result-object v0

    const/16 v1, 0x11d8

    :goto_0
    const/16 v2, 0x19a8

    if-ge v1, v2, :cond_1

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v2

    invoke-interface {v0, v2}, Ljava/util/Set;->contains(Ljava/lang/Object;)Z

    move-result v2

    if-nez v2, :cond_0

    goto :goto_1

    :cond_0
    add-int/lit8 v1, v1, 0x1

    goto :goto_0

    :cond_1
    const v1, 0x7fffffff

    :goto_1
    new-instance v0, Landroid/content/Intent;

    invoke-direct {v0}, Landroid/content/Intent;-><init>()V

    const-string v2, "android.media.action.IMAGE_CAPTURE"

    invoke-virtual {v0, v2}, Landroid/content/Intent;->setAction(Ljava/lang/String;)Landroid/content/Intent;

    new-instance v2, Ljava/io/File;

    new-instance v3, Ljava/lang/StringBuilder;

    invoke-direct {v3}, Ljava/lang/StringBuilder;-><init>()V

    invoke-virtual {p1}, Landroidx/fragment/app/Fragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v4

    invoke-virtual {v4}, Landroidx/fragment/app/FragmentActivity;->getExternalCacheDir()Ljava/io/File;

    move-result-object v4

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    sget-object v4, Ljava/io/File;->separator:Ljava/lang/String;

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    sget-object v4, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v5, "image-%s.jpg"

    const/4 v6, 0x1

    new-array v6, v6, [Ljava/lang/Object;

    const/4 v7, 0x0

    invoke-static {}, Ljava/lang/System;->currentTimeMillis()J

    move-result-wide v8

    invoke-static {v8, v9}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v8

    aput-object v8, v6, v7

    invoke-static {v4, v5, v6}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v4

    invoke-virtual {v3, v4}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v3}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-direct {v2, v3}, Ljava/io/File;-><init>(Ljava/lang/String;)V

    iget-object v3, p0, Lcom/zopim/android/sdk/attachment/ImagePicker;->mCameraImages:Ljava/util/Map;

    invoke-static {v1}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object v4

    invoke-interface {v3, v4, v2}, Ljava/util/Map;->put(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;

    const-string v3, "output"

    invoke-static {v2}, Landroid/net/Uri;->fromFile(Ljava/io/File;)Landroid/net/Uri;

    move-result-object v2

    invoke-virtual {v0, v3, v2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Landroid/os/Parcelable;)Landroid/content/Intent;

    invoke-virtual {p1, v0, v1}, Landroidx/fragment/app/Fragment;->startActivityForResult(Landroid/content/Intent;I)V

    return-void
.end method

.method private pickImageFromGalleryInternal(Landroidx/fragment/app/Fragment;)V
    .locals 3
    .annotation build Landroid/annotation/TargetApi;
        value = 0x12
    .end annotation

    new-instance v0, Landroid/content/Intent;

    const-string v1, "android.intent.action.GET_CONTENT"

    invoke-direct {v0, v1}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    sget v1, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v2, 0x12

    if-lt v1, v2, :cond_0

    const-string v1, "android.intent.extra.ALLOW_MULTIPLE"

    const/4 v2, 0x0

    invoke-virtual {v0, v1, v2}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Z)Landroid/content/Intent;

    :cond_0
    const-string v1, "android.intent.action.GET_CONTENT"

    invoke-virtual {v0, v1}, Landroid/content/Intent;->setAction(Ljava/lang/String;)Landroid/content/Intent;

    move-result-object v0

    const-string v1, "image/*"

    invoke-virtual {v0, v1}, Landroid/content/Intent;->setType(Ljava/lang/String;)Landroid/content/Intent;

    const/16 v1, 0x11d7

    invoke-virtual {p1, v0, v1}, Landroidx/fragment/app/Fragment;->startActivityForResult(Landroid/content/Intent;I)V

    return-void
.end method

.method public static valueOf(Ljava/lang/String;)Lcom/zopim/android/sdk/attachment/ImagePicker;
    .locals 1

    const-class v0, Lcom/zopim/android/sdk/attachment/ImagePicker;

    invoke-static {v0, p0}, Ljava/lang/Enum;->valueOf(Ljava/lang/Class;Ljava/lang/String;)Ljava/lang/Enum;

    move-result-object p0

    check-cast p0, Lcom/zopim/android/sdk/attachment/ImagePicker;

    return-object p0
.end method

.method public static values()[Lcom/zopim/android/sdk/attachment/ImagePicker;
    .locals 1

    sget-object v0, Lcom/zopim/android/sdk/attachment/ImagePicker;->$VALUES:[Lcom/zopim/android/sdk/attachment/ImagePicker;

    invoke-virtual {v0}, [Lcom/zopim/android/sdk/attachment/ImagePicker;->clone()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, [Lcom/zopim/android/sdk/attachment/ImagePicker;

    return-object v0
.end method


# virtual methods
.method public getFilesFromActivityOnResult(Landroid/content/Context;IILandroid/content/Intent;Lcom/zopim/android/sdk/attachment/ImagePicker$Callback;)V
    .locals 3

    const/4 v0, -0x1

    if-ne p3, v0, :cond_2

    const/16 p3, 0x11d7

    const/4 v0, 0x0

    const/4 v1, 0x1

    if-ne p2, p3, :cond_0

    invoke-direct {p0, p4}, Lcom/zopim/android/sdk/attachment/ImagePicker;->extractUrisFromIntent(Landroid/content/Intent;)Ljava/util/List;

    move-result-object p2

    new-instance p3, Lcom/zopim/android/sdk/attachment/ImagePicker$a;

    invoke-direct {p3, p0, p5}, Lcom/zopim/android/sdk/attachment/ImagePicker$a;-><init>(Lcom/zopim/android/sdk/attachment/ImagePicker;Lcom/zopim/android/sdk/attachment/ImagePicker$Callback;)V

    new-array p4, v1, [Landroid/util/Pair;

    new-instance p5, Landroid/util/Pair;

    invoke-direct {p5, p1, p2}, Landroid/util/Pair;-><init>(Ljava/lang/Object;Ljava/lang/Object;)V

    aput-object p5, p4, v0

    invoke-virtual {p3, p4}, Lcom/zopim/android/sdk/attachment/ImagePicker$a;->execute([Ljava/lang/Object;)Landroid/os/AsyncTask;

    goto :goto_1

    :cond_0
    iget-object p1, p0, Lcom/zopim/android/sdk/attachment/ImagePicker;->mCameraImages:Ljava/util/Map;

    invoke-static {p2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p3

    invoke-interface {p1, p3}, Ljava/util/Map;->containsKey(Ljava/lang/Object;)Z

    move-result p1

    if-eqz p1, :cond_1

    iget-object p1, p0, Lcom/zopim/android/sdk/attachment/ImagePicker;->mCameraImages:Ljava/util/Map;

    invoke-static {p2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p3

    invoke-interface {p1, p3}, Ljava/util/Map;->get(Ljava/lang/Object;)Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Ljava/io/File;

    iget-object p3, p0, Lcom/zopim/android/sdk/attachment/ImagePicker;->mCameraImages:Ljava/util/Map;

    invoke-static {p2}, Ljava/lang/Integer;->valueOf(I)Ljava/lang/Integer;

    move-result-object p2

    invoke-interface {p3, p2}, Ljava/util/Map;->remove(Ljava/lang/Object;)Ljava/lang/Object;

    sget-object p2, Lcom/zopim/android/sdk/attachment/ImagePicker;->LOG_TAG:Ljava/lang/String;

    sget-object p3, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string p4, "Image from camera: %s\n"

    new-array v1, v1, [Ljava/lang/Object;

    invoke-virtual {p1}, Ljava/io/File;->getAbsolutePath()Ljava/lang/String;

    move-result-object v2

    aput-object v2, v1, v0

    invoke-static {p3, p4, v1}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object p3

    invoke-static {p2, p3}, Lcom/zopim/android/sdk/api/Logger;->d(Ljava/lang/String;Ljava/lang/String;)V

    new-instance p2, Lcom/zopim/android/sdk/attachment/a;

    invoke-direct {p2, p0, p1}, Lcom/zopim/android/sdk/attachment/a;-><init>(Lcom/zopim/android/sdk/attachment/ImagePicker;Ljava/io/File;)V

    invoke-direct {p0, p2, p5}, Lcom/zopim/android/sdk/attachment/ImagePicker;->answerCallback(Ljava/util/List;Lcom/zopim/android/sdk/attachment/ImagePicker$Callback;)V

    goto :goto_1

    :cond_1
    new-instance p1, Ljava/util/ArrayList;

    invoke-direct {p1}, Ljava/util/ArrayList;-><init>()V

    goto :goto_0

    :cond_2
    new-instance p1, Ljava/util/ArrayList;

    invoke-direct {p1}, Ljava/util/ArrayList;-><init>()V

    :goto_0
    invoke-direct {p0, p1, p5}, Lcom/zopim/android/sdk/attachment/ImagePicker;->answerCallback(Ljava/util/List;Lcom/zopim/android/sdk/attachment/ImagePicker$Callback;)V

    :goto_1
    return-void
.end method

.method public hasPermissionForCamera(Landroid/content/Context;)Z
    .locals 10

    new-instance v0, Landroid/content/Intent;

    invoke-direct {v0}, Landroid/content/Intent;-><init>()V

    const-string v1, "android.media.action.IMAGE_CAPTURE"

    invoke-virtual {v0, v1}, Landroid/content/Intent;->setAction(Ljava/lang/String;)Landroid/content/Intent;

    invoke-virtual {p1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v1

    const-string v2, "android.hardware.camera"

    invoke-virtual {v1, v2}, Landroid/content/pm/PackageManager;->hasSystemFeature(Ljava/lang/String;)Z

    move-result v1

    const/4 v2, 0x0

    const/4 v3, 0x1

    if-nez v1, :cond_1

    invoke-virtual {p1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v1

    const-string v4, "android.hardware.camera.front"

    invoke-virtual {v1, v4}, Landroid/content/pm/PackageManager;->hasSystemFeature(Ljava/lang/String;)Z

    move-result v1

    if-eqz v1, :cond_0

    goto :goto_0

    :cond_0
    const/4 v1, 0x0

    goto :goto_1

    :cond_1
    :goto_0
    const/4 v1, 0x1

    :goto_1
    invoke-virtual {p1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v4

    invoke-virtual {v0, v4}, Landroid/content/Intent;->resolveActivity(Landroid/content/pm/PackageManager;)Landroid/content/ComponentName;

    move-result-object v0

    if-eqz v0, :cond_2

    const/4 v0, 0x1

    goto :goto_2

    :cond_2
    const/4 v0, 0x0

    :goto_2
    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/attachment/ImagePicker;->hasExternalReadPermission(Landroid/content/Context;)Z

    move-result p1

    sget-object v4, Lcom/zopim/android/sdk/attachment/ImagePicker;->LOG_TAG:Ljava/lang/String;

    sget-object v5, Ljava/util/Locale;->US:Ljava/util/Locale;

    const-string v6, "Camera permissions: camera present: %b, camera app present: %b, external storage read permission: %b"

    const/4 v7, 0x3

    new-array v7, v7, [Ljava/lang/Object;

    invoke-static {v1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v8

    aput-object v8, v7, v2

    invoke-static {v0}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v8

    aput-object v8, v7, v3

    const/4 v8, 0x2

    invoke-static {p1}, Ljava/lang/Boolean;->valueOf(Z)Ljava/lang/Boolean;

    move-result-object v9

    aput-object v9, v7, v8

    invoke-static {v5, v6, v7}, Ljava/lang/String;->format(Ljava/util/Locale;Ljava/lang/String;[Ljava/lang/Object;)Ljava/lang/String;

    move-result-object v5

    invoke-static {v4, v5}, Lcom/zopim/android/sdk/api/Logger;->d(Ljava/lang/String;Ljava/lang/String;)V

    if-eqz v1, :cond_3

    if-eqz v0, :cond_3

    if-eqz p1, :cond_3

    const/4 v2, 0x1

    :cond_3
    return v2
.end method

.method public hasPermissionForGallery(Landroid/content/Context;)Z
    .locals 0

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/attachment/ImagePicker;->hasExternalReadPermission(Landroid/content/Context;)Z

    move-result p1

    return p1
.end method

.method public pickImageFromCamera(Landroidx/fragment/app/Fragment;)V
    .locals 1

    invoke-virtual {p1}, Landroidx/fragment/app/Fragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/zopim/android/sdk/attachment/ImagePicker;->hasPermissionForCamera(Landroid/content/Context;)Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/attachment/ImagePicker;->pickImageFromCameraInternal(Landroidx/fragment/app/Fragment;)V

    :cond_0
    return-void
.end method

.method public pickImagesFromGallery(Landroidx/fragment/app/Fragment;)V
    .locals 1

    invoke-virtual {p1}, Landroidx/fragment/app/Fragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v0

    invoke-virtual {p0, v0}, Lcom/zopim/android/sdk/attachment/ImagePicker;->hasPermissionForGallery(Landroid/content/Context;)Z

    move-result v0

    if-eqz v0, :cond_0

    invoke-direct {p0, p1}, Lcom/zopim/android/sdk/attachment/ImagePicker;->pickImageFromGalleryInternal(Landroidx/fragment/app/Fragment;)V

    goto :goto_0

    :cond_0
    sget-object p1, Lcom/zopim/android/sdk/attachment/ImagePicker;->LOG_TAG:Ljava/lang/String;

    const-string v0, "Image picker needs READ_EXTERNAL_STORAGE permission. Have you declared that permission in your manifest file?"

    invoke-static {p1, v0}, Landroid/util/Log;->w(Ljava/lang/String;Ljava/lang/String;)I

    :goto_0
    return-void
.end method
