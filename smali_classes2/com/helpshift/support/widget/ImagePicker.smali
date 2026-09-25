.class public Lcom/helpshift/support/widget/ImagePicker;
.super Ljava/lang/Object;
.source "ImagePicker.java"


# annotations
.annotation system Ldalvik/annotation/MemberClasses;
    value = {
        Lcom/helpshift/support/widget/ImagePicker$ImagePickerListener;
    }
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "<T:",
        "Landroidx/fragment/app/Fragment;",
        ":",
        "Lcom/helpshift/support/widget/ImagePicker$ImagePickerListener;",
        ">",
        "Ljava/lang/Object;"
    }
.end annotation


# static fields
.field public static final IMAGE_FILE_NOT_FOUND:I = -0x1

.field public static final IMAGE_FILE_SIZE_LIMIT_EXCEEDED:I = -0x3

.field public static final INVALID_IMAGE_URI:I = -0x2

.field private static final MAX_IMAGE_FILE_SIZE_LIMIT:J = 0x1900000L

.field public static final NO_APPS_TO_OPEN_IMAGES_INTENT:I = -0x4

.field public static final PICK_IMAGE_REQUEST_ID:I = 0x1

.field public static final PICK_IMAGE_WITHOUT_PERMISSIONS_REQUEST_ID:I = 0x2

.field private static final TAG:Ljava/lang/String; = "Helpshift_ImagePicker"


# instance fields
.field private final KEY_EXTRA_DATA:Ljava/lang/String;

.field private device:Lcom/helpshift/common/platform/Device;

.field private extraData:Landroid/os/Bundle;

.field private imagePickerListenerRef:Ljava/lang/ref/WeakReference;
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "Ljava/lang/ref/WeakReference<",
            "TT;>;"
        }
    .end annotation
.end field


# direct methods
.method public constructor <init>(Landroidx/fragment/app/Fragment;)V
    .locals 1
    .annotation system Ldalvik/annotation/Signature;
        value = {
            "(TT;)V"
        }
    .end annotation

    .line 53
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    const-string v0, "key_extra_data"

    .line 48
    iput-object v0, p0, Lcom/helpshift/support/widget/ImagePicker;->KEY_EXTRA_DATA:Ljava/lang/String;

    .line 54
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getPlatform()Lcom/helpshift/common/platform/Platform;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getDevice()Lcom/helpshift/common/platform/Device;

    move-result-object v0

    iput-object v0, p0, Lcom/helpshift/support/widget/ImagePicker;->device:Lcom/helpshift/common/platform/Device;

    .line 55
    new-instance v0, Ljava/lang/ref/WeakReference;

    invoke-direct {v0, p1}, Ljava/lang/ref/WeakReference;-><init>(Ljava/lang/Object;)V

    iput-object v0, p0, Lcom/helpshift/support/widget/ImagePicker;->imagePickerListenerRef:Ljava/lang/ref/WeakReference;

    return-void
.end method

.method private createImagePickerFileFromUri(Landroid/net/Uri;)Lcom/helpshift/conversation/dto/ImagePickerFile;
    .locals 7

    .line 195
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    .line 196
    invoke-virtual {v0}, Landroid/content/Context;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object v1

    const/4 v3, 0x0

    const/4 v4, 0x0

    const/4 v5, 0x0

    const/4 v6, 0x0

    move-object v2, p1

    .line 197
    invoke-virtual/range {v1 .. v6}, Landroid/content/ContentResolver;->query(Landroid/net/Uri;[Ljava/lang/String;Ljava/lang/String;[Ljava/lang/String;Ljava/lang/String;)Landroid/database/Cursor;

    move-result-object v0

    const/4 v1, 0x0

    if-eqz v0, :cond_2

    .line 201
    :try_start_0
    invoke-interface {v0}, Landroid/database/Cursor;->moveToFirst()Z

    move-result v2

    if-eqz v2, :cond_2

    const-string v2, "_display_name"

    .line 202
    invoke-interface {v0, v2}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v2

    invoke-interface {v0, v2}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v2

    .line 203
    invoke-static {v2}, Lcom/helpshift/common/StringUtils;->isEmpty(Ljava/lang/String;)Z

    move-result v3

    if-eqz v3, :cond_0

    .line 204
    invoke-static {}, Ljava/util/UUID;->randomUUID()Ljava/util/UUID;

    move-result-object v2

    invoke-virtual {v2}, Ljava/util/UUID;->toString()Ljava/lang/String;

    move-result-object v2

    :cond_0
    const-string v3, "_size"

    .line 206
    invoke-interface {v0, v3}, Landroid/database/Cursor;->getColumnIndex(Ljava/lang/String;)I

    move-result v3

    .line 207
    invoke-interface {v0, v3}, Landroid/database/Cursor;->isNull(I)Z

    move-result v4

    if-nez v4, :cond_3

    .line 208
    invoke-interface {v0, v3}, Landroid/database/Cursor;->getString(I)Ljava/lang/String;

    move-result-object v3
    :try_end_0
    .catchall {:try_start_0 .. :try_end_0} :catchall_0

    if-eqz v3, :cond_3

    .line 211
    :try_start_1
    invoke-static {v3}, Ljava/lang/Long;->valueOf(Ljava/lang/String;)Ljava/lang/Long;

    move-result-object v3
    :try_end_1
    .catch Ljava/lang/NumberFormatException; {:try_start_1 .. :try_end_1} :catch_0
    .catchall {:try_start_1 .. :try_end_1} :catchall_0

    move-object v1, v3

    goto :goto_0

    :catch_0
    move-exception v3

    :try_start_2
    const-string v4, "Helpshift_ImagePicker"

    .line 214
    new-instance v5, Ljava/lang/StringBuilder;

    invoke-direct {v5}, Ljava/lang/StringBuilder;-><init>()V

    const-string v6, "Error getting size due to "

    invoke-virtual {v5, v6}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v5, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v5}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v3

    invoke-static {v4, v3}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V
    :try_end_2
    .catchall {:try_start_2 .. :try_end_2} :catchall_0

    goto :goto_0

    :catchall_0
    move-exception p1

    if-eqz v0, :cond_1

    .line 222
    invoke-interface {v0}, Landroid/database/Cursor;->close()V

    .line 224
    :cond_1
    throw p1

    :cond_2
    move-object v2, v1

    :cond_3
    :goto_0
    if-eqz v0, :cond_4

    .line 222
    invoke-interface {v0}, Landroid/database/Cursor;->close()V

    .line 225
    :cond_4
    new-instance v0, Lcom/helpshift/conversation/dto/ImagePickerFile;

    invoke-direct {v0, p1, v2, v1}, Lcom/helpshift/conversation/dto/ImagePickerFile;-><init>(Ljava/lang/Object;Ljava/lang/String;Ljava/lang/Long;)V

    return-object v0
.end method

.method private isImageUriValid(Landroid/net/Uri;)Z
    .locals 7

    .line 229
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    .line 230
    invoke-virtual {v0}, Landroid/content/Context;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object v0

    .line 231
    invoke-virtual {v0, p1}, Landroid/content/ContentResolver;->getType(Landroid/net/Uri;)Ljava/lang/String;

    move-result-object p1

    const-string v0, "image/jpeg"

    const-string v1, "image/png"

    const-string v2, "image/gif"

    const-string v3, "image/x-png"

    const-string v4, "image/x-citrix-pjpeg"

    const-string v5, "image/x-citrix-gif"

    const-string v6, "image/pjpeg"

    .line 232
    filled-new-array/range {v0 .. v6}, [Ljava/lang/String;

    move-result-object v0

    .line 239
    new-instance v1, Ljava/util/HashSet;

    invoke-static {v0}, Ljava/util/Arrays;->asList([Ljava/lang/Object;)Ljava/util/List;

    move-result-object v0

    invoke-direct {v1, v0}, Ljava/util/HashSet;-><init>(Ljava/util/Collection;)V

    .line 240
    invoke-interface {v1, p1}, Ljava/util/Set;->contains(Ljava/lang/Object;)Z

    move-result p1

    return p1
.end method

.method private launchImagePicker(Landroid/os/Bundle;I)V
    .locals 5

    .line 101
    iput-object p1, p0, Lcom/helpshift/support/widget/ImagePicker;->extraData:Landroid/os/Bundle;

    const-string p1, "Helpshift_ImagePicker"

    .line 102
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Launching attachment picker now, flowRequestCode: "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p2}, Ljava/lang/StringBuilder;->append(I)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {p1, v0}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 103
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getApplicationContext()Landroid/content/Context;

    move-result-object p1

    .line 104
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getPlatform()Lcom/helpshift/common/platform/Platform;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/common/platform/Platform;->getDevice()Lcom/helpshift/common/platform/Device;

    move-result-object v0

    invoke-interface {v0}, Lcom/helpshift/common/platform/Device;->getOSVersionNumber()I

    move-result v0

    const/4 v1, 0x1

    const/4 v2, 0x2

    if-ne p2, v2, :cond_0

    const/16 v2, 0x13

    if-lt v0, v2, :cond_0

    .line 108
    new-instance v0, Landroid/content/Intent;

    const-string v2, "android.intent.action.OPEN_DOCUMENT"

    invoke-direct {v0, v2}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    const-string v2, "image/*"

    .line 109
    invoke-virtual {v0, v2}, Landroid/content/Intent;->setType(Ljava/lang/String;)Landroid/content/Intent;

    .line 110
    invoke-virtual {v0, v1}, Landroid/content/Intent;->addFlags(I)Landroid/content/Intent;

    const-string v2, "android.intent.extra.LOCAL_ONLY"

    .line 111
    invoke-virtual {v0, v2, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Z)Landroid/content/Intent;

    goto :goto_0

    .line 114
    :cond_0
    new-instance v2, Landroid/content/Intent;

    const-string v3, "android.intent.action.PICK"

    sget-object v4, Landroid/provider/MediaStore$Images$Media;->EXTERNAL_CONTENT_URI:Landroid/net/Uri;

    invoke-direct {v2, v3, v4}, Landroid/content/Intent;-><init>(Ljava/lang/String;Landroid/net/Uri;)V

    .line 116
    invoke-virtual {p1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object v3

    invoke-virtual {v2, v3}, Landroid/content/Intent;->resolveActivity(Landroid/content/pm/PackageManager;)Landroid/content/ComponentName;

    move-result-object v3

    if-eqz v3, :cond_1

    .line 117
    invoke-direct {p0, v2, p2}, Lcom/helpshift/support/widget/ImagePicker;->startActivityForResult(Landroid/content/Intent;I)V

    return-void

    .line 120
    :cond_1
    new-instance v2, Landroid/content/Intent;

    const-string v3, "android.intent.action.GET_CONTENT"

    invoke-direct {v2, v3}, Landroid/content/Intent;-><init>(Ljava/lang/String;)V

    const-string v3, "image/*"

    .line 121
    invoke-virtual {v2, v3}, Landroid/content/Intent;->setType(Ljava/lang/String;)Landroid/content/Intent;

    const/16 v3, 0xb

    if-lt v0, v3, :cond_2

    const-string v0, "android.intent.extra.LOCAL_ONLY"

    .line 123
    invoke-virtual {v2, v0, v1}, Landroid/content/Intent;->putExtra(Ljava/lang/String;Z)Landroid/content/Intent;

    :cond_2
    move-object v0, v2

    .line 126
    :goto_0
    invoke-virtual {p1}, Landroid/content/Context;->getPackageManager()Landroid/content/pm/PackageManager;

    move-result-object p1

    invoke-virtual {v0, p1}, Landroid/content/Intent;->resolveActivity(Landroid/content/pm/PackageManager;)Landroid/content/ComponentName;

    move-result-object p1

    if-nez p1, :cond_3

    const-string p1, "Helpshift_ImagePicker"

    .line 127
    new-instance p2, Ljava/lang/StringBuilder;

    invoke-direct {p2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "No app found for handling image pick intent "

    invoke-virtual {p2, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {p2, v0}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {p2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p2

    invoke-static {p1, p2}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;)V

    const/4 p1, -0x4

    const/4 p2, 0x0

    .line 128
    invoke-direct {p0, p1, p2}, Lcom/helpshift/support/widget/ImagePicker;->sendImagePickerResultFailureCallback(ILjava/lang/Long;)V

    goto :goto_1

    .line 131
    :cond_3
    invoke-direct {p0, v0, p2}, Lcom/helpshift/support/widget/ImagePicker;->startActivityForResult(Landroid/content/Intent;I)V

    :goto_1
    return-void
.end method

.method private processUriForAttachment(Landroid/net/Uri;)V
    .locals 8

    .line 165
    invoke-direct {p0, p1}, Lcom/helpshift/support/widget/ImagePicker;->isImageUriValid(Landroid/net/Uri;)Z

    move-result v0

    const/4 v1, 0x0

    if-eqz v0, :cond_3

    .line 166
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getApplicationContext()Landroid/content/Context;

    move-result-object v0

    .line 167
    invoke-static {p1, v0}, Lcom/helpshift/util/FileUtil;->doesFileFromUriExistAndCanRead(Landroid/net/Uri;Landroid/content/Context;)Z

    move-result v2

    if-eqz v2, :cond_2

    .line 169
    invoke-direct {p0, p1}, Lcom/helpshift/support/widget/ImagePicker;->createImagePickerFileFromUri(Landroid/net/Uri;)Lcom/helpshift/conversation/dto/ImagePickerFile;

    move-result-object v1

    .line 170
    iget-object v2, v1, Lcom/helpshift/conversation/dto/ImagePickerFile;->originalFileSize:Ljava/lang/Long;

    if-eqz v2, :cond_1

    .line 171
    invoke-virtual {v2}, Ljava/lang/Long;->longValue()J

    move-result-wide v3

    const-wide/32 v5, 0x1900000

    cmp-long v7, v3, v5

    if-lez v7, :cond_1

    .line 172
    invoke-static {p1, v0}, Lcom/helpshift/util/ImageUtil;->isResizableImage(Landroid/net/Uri;Landroid/content/Context;)Z

    move-result v0

    if-eqz v0, :cond_0

    goto :goto_0

    :cond_0
    const-string p1, "Helpshift_ImagePicker"

    .line 177
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Image picker file size limit exceeded (in bytes): "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, v2}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    const-string v1, ", returning failure"

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object v0

    invoke-static {p1, v0}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    const/4 p1, -0x3

    .line 180
    invoke-static {v5, v6}, Ljava/lang/Long;->valueOf(J)Ljava/lang/Long;

    move-result-object v0

    .line 179
    invoke-direct {p0, p1, v0}, Lcom/helpshift/support/widget/ImagePicker;->sendImagePickerResultFailureCallback(ILjava/lang/Long;)V

    goto :goto_1

    :cond_1
    :goto_0
    const-string v0, "Helpshift_ImagePicker"

    .line 173
    new-instance v2, Ljava/lang/StringBuilder;

    invoke-direct {v2}, Ljava/lang/StringBuilder;-><init>()V

    const-string v3, "Image picker result success, path: "

    invoke-virtual {v2, v3}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v2, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v2}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {v0, p1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 174
    iget-object p1, p0, Lcom/helpshift/support/widget/ImagePicker;->extraData:Landroid/os/Bundle;

    invoke-direct {p0, v1, p1}, Lcom/helpshift/support/widget/ImagePicker;->sendImagePickerResultSuccessCallback(Lcom/helpshift/conversation/dto/ImagePickerFile;Landroid/os/Bundle;)V

    goto :goto_1

    :cond_2
    const-string p1, "Helpshift_ImagePicker"

    const-string v0, "Image picker file reading error, returning failure"

    .line 184
    invoke-static {p1, v0}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    const/4 p1, -0x1

    .line 185
    invoke-direct {p0, p1, v1}, Lcom/helpshift/support/widget/ImagePicker;->sendImagePickerResultFailureCallback(ILjava/lang/Long;)V

    goto :goto_1

    :cond_3
    const-string p1, "Helpshift_ImagePicker"

    const-string v0, "Image picker file invalid mime type, returning failure"

    .line 189
    invoke-static {p1, v0}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    const/4 p1, -0x2

    .line 190
    invoke-direct {p0, p1, v1}, Lcom/helpshift/support/widget/ImagePicker;->sendImagePickerResultFailureCallback(ILjava/lang/Long;)V

    :goto_1
    return-void
.end method

.method private sendImagePickerResultFailureCallback(ILjava/lang/Long;)V
    .locals 1

    .line 253
    iget-object v0, p0, Lcom/helpshift/support/widget/ImagePicker;->imagePickerListenerRef:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroidx/fragment/app/Fragment;

    if-eqz v0, :cond_0

    .line 255
    check-cast v0, Lcom/helpshift/support/widget/ImagePicker$ImagePickerListener;

    invoke-interface {v0, p1, p2}, Lcom/helpshift/support/widget/ImagePicker$ImagePickerListener;->onImagePickerResultFailure(ILjava/lang/Long;)V

    :cond_0
    return-void
.end method

.method private sendImagePickerResultSuccessCallback(Lcom/helpshift/conversation/dto/ImagePickerFile;Landroid/os/Bundle;)V
    .locals 1

    .line 245
    iget-object v0, p0, Lcom/helpshift/support/widget/ImagePicker;->imagePickerListenerRef:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroidx/fragment/app/Fragment;

    if-eqz v0, :cond_0

    .line 247
    check-cast v0, Lcom/helpshift/support/widget/ImagePicker$ImagePickerListener;

    invoke-interface {v0, p1, p2}, Lcom/helpshift/support/widget/ImagePicker$ImagePickerListener;->onImagePickerResultSuccess(Lcom/helpshift/conversation/dto/ImagePickerFile;Landroid/os/Bundle;)V

    :cond_0
    return-void
.end method

.method private startActivityForResult(Landroid/content/Intent;I)V
    .locals 2

    .line 262
    :try_start_0
    iget-object v0, p0, Lcom/helpshift/support/widget/ImagePicker;->imagePickerListenerRef:Ljava/lang/ref/WeakReference;

    invoke-virtual {v0}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object v0

    check-cast v0, Landroidx/fragment/app/Fragment;

    if-eqz v0, :cond_0

    .line 263
    invoke-virtual {v0}, Landroidx/fragment/app/Fragment;->getActivity()Landroidx/fragment/app/FragmentActivity;

    move-result-object v1

    if-eqz v1, :cond_0

    .line 264
    invoke-virtual {v0, p1, p2}, Landroidx/fragment/app/Fragment;->startActivityForResult(Landroid/content/Intent;I)V
    :try_end_0
    .catch Landroid/content/ActivityNotFoundException; {:try_start_0 .. :try_end_0} :catch_0

    goto :goto_0

    :catch_0
    move-exception p1

    const-string p2, "Helpshift_ImagePicker"

    .line 268
    new-instance v0, Ljava/lang/StringBuilder;

    invoke-direct {v0}, Ljava/lang/StringBuilder;-><init>()V

    const-string v1, "Error occurred while starting app for handling image pick intent "

    invoke-virtual {v0, v1}, Ljava/lang/StringBuilder;->append(Ljava/lang/String;)Ljava/lang/StringBuilder;

    invoke-virtual {v0, p1}, Ljava/lang/StringBuilder;->append(Ljava/lang/Object;)Ljava/lang/StringBuilder;

    invoke-virtual {v0}, Ljava/lang/StringBuilder;->toString()Ljava/lang/String;

    move-result-object p1

    invoke-static {p2, p1}, Lcom/helpshift/util/HSLogger;->e(Ljava/lang/String;Ljava/lang/String;)V

    const/4 p1, -0x4

    const/4 p2, 0x0

    .line 269
    invoke-direct {p0, p1, p2}, Lcom/helpshift/support/widget/ImagePicker;->sendImagePickerResultFailureCallback(ILjava/lang/Long;)V

    :cond_0
    :goto_0
    return-void
.end method


# virtual methods
.method public checkPermissionAndLaunchImagePicker(Landroid/os/Bundle;)V
    .locals 3

    const-string v0, "Helpshift_ImagePicker"

    const-string v1, "Checking permission before launching attachment picker"

    .line 66
    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 67
    sget-object v0, Lcom/helpshift/support/widget/ImagePicker$1;->$SwitchMap$com$helpshift$common$platform$Device$PermissionState:[I

    iget-object v1, p0, Lcom/helpshift/support/widget/ImagePicker;->device:Lcom/helpshift/common/platform/Device;

    sget-object v2, Lcom/helpshift/common/platform/Device$PermissionType;->READ_STORAGE:Lcom/helpshift/common/platform/Device$PermissionType;

    invoke-interface {v1, v2}, Lcom/helpshift/common/platform/Device;->checkPermission(Lcom/helpshift/common/platform/Device$PermissionType;)Lcom/helpshift/common/platform/Device$PermissionState;

    move-result-object v1

    invoke-virtual {v1}, Lcom/helpshift/common/platform/Device$PermissionState;->ordinal()I

    move-result v1

    aget v0, v0, v1

    packed-switch v0, :pswitch_data_0

    goto :goto_0

    :pswitch_0
    const-string p1, "Helpshift_ImagePicker"

    const-string v0, "READ_STORAGE permission is not granted but can be requested"

    .line 78
    invoke-static {p1, v0}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 80
    iget-object p1, p0, Lcom/helpshift/support/widget/ImagePicker;->imagePickerListenerRef:Ljava/lang/ref/WeakReference;

    invoke-virtual {p1}, Ljava/lang/ref/WeakReference;->get()Ljava/lang/Object;

    move-result-object p1

    check-cast p1, Landroidx/fragment/app/Fragment;

    if-eqz p1, :cond_0

    .line 82
    check-cast p1, Lcom/helpshift/support/widget/ImagePicker$ImagePickerListener;

    invoke-interface {p1}, Lcom/helpshift/support/widget/ImagePicker$ImagePickerListener;->askForReadStoragePermission()V

    goto :goto_0

    :pswitch_1
    const-string v0, "Helpshift_ImagePicker"

    const-string v1, "READ_STORAGE permission is not granted and can\'t be requested, starting alternate flow"

    .line 72
    invoke-static {v0, v1}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    const/4 v0, 0x2

    .line 75
    invoke-direct {p0, p1, v0}, Lcom/helpshift/support/widget/ImagePicker;->launchImagePicker(Landroid/os/Bundle;I)V

    goto :goto_0

    .line 69
    :pswitch_2
    invoke-virtual {p0, p1}, Lcom/helpshift/support/widget/ImagePicker;->launchImagePicker(Landroid/os/Bundle;)V

    :cond_0
    :goto_0
    return-void

    :pswitch_data_0
    .packed-switch 0x1
        :pswitch_2
        :pswitch_1
        :pswitch_0
    .end packed-switch
.end method

.method public launchImagePicker(Landroid/os/Bundle;)V
    .locals 1

    const/4 v0, 0x1

    .line 97
    invoke-direct {p0, p1, v0}, Lcom/helpshift/support/widget/ImagePicker;->launchImagePicker(Landroid/os/Bundle;I)V

    return-void
.end method

.method public onImagePickRequestResult(ILandroid/content/Intent;)V
    .locals 3

    .line 143
    invoke-virtual {p2}, Landroid/content/Intent;->getData()Landroid/net/Uri;

    move-result-object v0

    const/4 v1, 0x1

    if-ne p1, v1, :cond_0

    const-string p1, "Helpshift_ImagePicker"

    const-string p2, "Processing image uri with flow when permissions are available"

    .line 145
    invoke-static {p1, p2}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 146
    invoke-direct {p0, v0}, Lcom/helpshift/support/widget/ImagePicker;->processUriForAttachment(Landroid/net/Uri;)V

    goto :goto_0

    :cond_0
    const/4 v2, 0x2

    if-ne p1, v2, :cond_2

    const-string p1, "Helpshift_ImagePicker"

    const-string v2, "Processing image uri with flow when permissions are not available"

    .line 149
    invoke-static {p1, v2}, Lcom/helpshift/util/HSLogger;->d(Ljava/lang/String;Ljava/lang/String;)V

    .line 150
    invoke-virtual {p2}, Landroid/content/Intent;->getFlags()I

    move-result p1

    and-int/2addr p1, v1

    .line 152
    sget p2, Landroid/os/Build$VERSION;->SDK_INT:I

    const/16 v1, 0x13

    if-lt p2, v1, :cond_1

    .line 153
    invoke-static {}, Lcom/helpshift/util/HelpshiftContext;->getApplicationContext()Landroid/content/Context;

    move-result-object p2

    invoke-virtual {p2}, Landroid/content/Context;->getContentResolver()Landroid/content/ContentResolver;

    move-result-object p2

    invoke-virtual {p2, v0, p1}, Landroid/content/ContentResolver;->takePersistableUriPermission(Landroid/net/Uri;I)V

    .line 155
    :cond_1
    invoke-direct {p0, v0}, Lcom/helpshift/support/widget/ImagePicker;->processUriForAttachment(Landroid/net/Uri;)V

    :cond_2
    :goto_0
    return-void
.end method

.method public onSaveInstanceState(Landroid/os/Bundle;)V
    .locals 2

    const-string v0, "key_extra_data"

    .line 274
    iget-object v1, p0, Lcom/helpshift/support/widget/ImagePicker;->extraData:Landroid/os/Bundle;

    invoke-virtual {p1, v0, v1}, Landroid/os/Bundle;->putBundle(Ljava/lang/String;Landroid/os/Bundle;)V

    return-void
.end method

.method public onViewStateRestored(Landroid/os/Bundle;)V
    .locals 1

    const-string v0, "key_extra_data"

    .line 278
    invoke-virtual {p1, v0}, Landroid/os/Bundle;->containsKey(Ljava/lang/String;)Z

    move-result v0

    if-eqz v0, :cond_0

    const-string v0, "key_extra_data"

    .line 279
    invoke-virtual {p1, v0}, Landroid/os/Bundle;->getBundle(Ljava/lang/String;)Landroid/os/Bundle;

    move-result-object p1

    iput-object p1, p0, Lcom/helpshift/support/widget/ImagePicker;->extraData:Landroid/os/Bundle;

    :cond_0
    return-void
.end method
