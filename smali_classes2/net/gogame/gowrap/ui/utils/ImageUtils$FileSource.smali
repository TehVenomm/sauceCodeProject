.class public Lnet/gogame/gowrap/ui/utils/ImageUtils$FileSource;
.super Ljava/lang/Object;
.source "ImageUtils.java"

# interfaces
.implements Lnet/gogame/gowrap/ui/utils/ImageUtils$Source;


# annotations
.annotation system Ldalvik/annotation/EnclosingClass;
    value = Lnet/gogame/gowrap/ui/utils/ImageUtils;
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x9
    name = "FileSource"
.end annotation


# instance fields
.field private final file:Ljava/io/File;


# direct methods
.method public constructor <init>(Ljava/io/File;)V
    .locals 0

    .line 111
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 113
    iput-object p1, p0, Lnet/gogame/gowrap/ui/utils/ImageUtils$FileSource;->file:Ljava/io/File;

    return-void
.end method


# virtual methods
.method public close()V
    .locals 0
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    return-void
.end method

.method public getInputStream()Ljava/io/InputStream;
    .locals 2
    .annotation system Ldalvik/annotation/Throws;
        value = {
            Ljava/io/IOException;
        }
    .end annotation

    .line 118
    new-instance v0, Ljava/io/FileInputStream;

    iget-object v1, p0, Lnet/gogame/gowrap/ui/utils/ImageUtils$FileSource;->file:Ljava/io/File;

    invoke-direct {v0, v1}, Ljava/io/FileInputStream;-><init>(Ljava/io/File;)V

    return-object v0
.end method
