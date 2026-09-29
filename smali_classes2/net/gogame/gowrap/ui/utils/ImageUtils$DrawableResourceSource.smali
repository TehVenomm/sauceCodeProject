.class public Lnet/gogame/gowrap/ui/utils/ImageUtils$DrawableResourceSource;
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
    name = "DrawableResourceSource"
.end annotation


# instance fields
.field private final context:Landroid/content/Context;

.field private final resourceId:I


# direct methods
.method public constructor <init>(Landroid/content/Context;I)V
    .locals 0

    .line 133
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    .line 135
    iput-object p1, p0, Lnet/gogame/gowrap/ui/utils/ImageUtils$DrawableResourceSource;->context:Landroid/content/Context;

    .line 136
    iput p2, p0, Lnet/gogame/gowrap/ui/utils/ImageUtils$DrawableResourceSource;->resourceId:I

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

    .line 141
    iget-object v0, p0, Lnet/gogame/gowrap/ui/utils/ImageUtils$DrawableResourceSource;->context:Landroid/content/Context;

    invoke-virtual {v0}, Landroid/content/Context;->getResources()Landroid/content/res/Resources;

    move-result-object v0

    iget v1, p0, Lnet/gogame/gowrap/ui/utils/ImageUtils$DrawableResourceSource;->resourceId:I

    invoke-virtual {v0, v1}, Landroid/content/res/Resources;->openRawResource(I)Ljava/io/InputStream;

    move-result-object v0

    return-object v0
.end method
