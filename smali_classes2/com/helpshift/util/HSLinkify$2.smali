.class final Lcom/helpshift/util/HSLinkify$2;
.super Ljava/lang/Object;
.source "HSLinkify.java"

# interfaces
.implements Ljava/util/Comparator;


# annotations
.annotation system Ldalvik/annotation/EnclosingMethod;
    value = Lcom/helpshift/util/HSLinkify;->addLinks(Landroid/text/Spannable;ILcom/helpshift/util/HSLinkify$LinkClickListener;)Z
.end annotation

.annotation system Ldalvik/annotation/InnerClass;
    accessFlags = 0x8
    name = null
.end annotation

.annotation system Ldalvik/annotation/Signature;
    value = {
        "Ljava/lang/Object;",
        "Ljava/util/Comparator<",
        "Lcom/helpshift/util/LinkSpec;",
        ">;"
    }
.end annotation


# direct methods
.method constructor <init>()V
    .locals 0

    .line 291
    invoke-direct {p0}, Ljava/lang/Object;-><init>()V

    return-void
.end method


# virtual methods
.method public final compare(Lcom/helpshift/util/LinkSpec;Lcom/helpshift/util/LinkSpec;)I
    .locals 4

    .line 293
    iget v0, p1, Lcom/helpshift/util/LinkSpec;->start:I

    iget v1, p2, Lcom/helpshift/util/LinkSpec;->start:I

    const/4 v2, -0x1

    if-ge v0, v1, :cond_0

    return v2

    .line 297
    :cond_0
    iget v0, p1, Lcom/helpshift/util/LinkSpec;->start:I

    iget v1, p2, Lcom/helpshift/util/LinkSpec;->start:I

    const/4 v3, 0x1

    if-le v0, v1, :cond_1

    return v3

    .line 301
    :cond_1
    iget v0, p1, Lcom/helpshift/util/LinkSpec;->end:I

    iget v1, p2, Lcom/helpshift/util/LinkSpec;->end:I

    if-ge v0, v1, :cond_2

    return v3

    .line 305
    :cond_2
    iget p1, p1, Lcom/helpshift/util/LinkSpec;->end:I

    iget p2, p2, Lcom/helpshift/util/LinkSpec;->end:I

    if-le p1, p2, :cond_3

    return v2

    :cond_3
    const/4 p1, 0x0

    return p1
.end method

.method public bridge synthetic compare(Ljava/lang/Object;Ljava/lang/Object;)I
    .locals 0

    .line 291
    check-cast p1, Lcom/helpshift/util/LinkSpec;

    check-cast p2, Lcom/helpshift/util/LinkSpec;

    invoke-virtual {p0, p1, p2}, Lcom/helpshift/util/HSLinkify$2;->compare(Lcom/helpshift/util/LinkSpec;Lcom/helpshift/util/LinkSpec;)I

    move-result p1

    return p1
.end method

.method public final equals(Ljava/lang/Object;)Z
    .locals 0

    const/4 p1, 0x0

    return p1
.end method
