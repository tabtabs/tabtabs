/******************************************************************************
 * Spine Runtimes License Agreement
 * Last updated September 24, 2021. Replaces all prior versions.
 *
 * Copyright (c) 2013-2021, Esoteric Software LLC
 *
 * Integration of the Spine Runtimes into software or otherwise creating
 * derivative works of the Spine Runtimes is permitted under the terms and
 * conditions of Section 2 of the Spine Editor License Agreement:
 * http://esotericsoftware.com/spine-editor-license
 *
 * Otherwise, it is permitted to integrate the Spine Runtimes into software
 * or otherwise create derivative works of the Spine Runtimes (collectively,
 * "Products"), provided that each user of the Products must obtain their own
 * Spine Editor license and redistribution of the Products in any form must
 * include this license and copyright notice.
 *
 * THE SPINE RUNTIMES ARE PROVIDED BY ESOTERIC SOFTWARE LLC "AS IS" AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL ESOTERIC SOFTWARE LLC BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES,
 * BUSINESS INTERRUPTION, OR LOSS OF USE, DATA, OR PROFITS) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
 * THE SPINE RUNTIMES, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *****************************************************************************/

using Spine.Unity;
using System.Collections;
using UnityEngine;

namespace Spine.Unity.Examples 
{
	public class Raptor : MonoBehaviour
	{

		#region Inspector
		public AnimationReferenceAsset walk;
		public AnimationReferenceAsset gungrab;
		public AnimationReferenceAsset gunkeep;
		#endregion

		SkeletonAnimation skeletonAnimation;

		void Start ()
		{
			skeletonAnimation = GetComponent<SkeletonAnimation>();
			StartCoroutine(GunGrabRoutine());
		}

		IEnumerator GunGrabRoutine ()
		{
			// 트랙 0에서 걷기 애니메이션을 재생합니다.
			skeletonAnimation.AnimationState.SetAnimation(0, walk, true);

			// 트랙 1에서 gungrab 및 gunkeep 애니메이션을 반복 재생합니다.
			while (true)
			{
				yield return new WaitForSeconds(Random.Range(0.5f, 3f));
				skeletonAnimation.AnimationState.SetAnimation(1, gungrab, false);

				yield return new WaitForSeconds(Random.Range(0.5f, 3f));
				skeletonAnimation.AnimationState.SetAnimation(1, gunkeep, false);
			}
		}

	}
}
